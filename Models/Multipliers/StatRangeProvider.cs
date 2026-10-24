using Models.Stats;

namespace Models.Multipliers;

// Подсказка диапазона (мин–макс) и значения характеристики для ручного редактора статов.
//
// Диапазон считается через уже существующие в проекте коэффициенты:
//   множитель вещи (зелёные статы: тип брони / одно-двуручное оружие / бижутерия)
//   × множитель редкости (если есть) × множитель руны улучшения.
// Эталон для калибровки — реальный предмет «Улучшенный боевой ледяной посох +10»
// (двуручное оружие ×4, руна +10 ×2, то есть общий множитель 8):
//   Сила атаки          1326–1989
//   Критический урон    1474–2211
//   Здоровье            7370–11055
//   Сопр. крит. урону   1474–2211
// В игре максимум диапазона всегда ровно в 1.5 раза больше минимума.
public static class StatRangeProvider
{
    // Общий множитель (зелёный × руна) эталонного предмета: двуручное оружие +10 → 4 × 2.
    private const double ReferenceScale = 8;

    // Во сколько раз максимум диапазона больше минимума (подтверждено по реальному предмету).
    private const double MaxToMinRatio = 1.5;

    // Среднее по эталону: минимум ≈ базовое число стата (из таблицы для 3 статов) × это значение
    // при ReferenceScale. Совпадает с реальными числами в пределах нескольких единиц; используется
    // для характеристик, которых ещё нет в таблице точных значений ниже.
    private const double MinPerBaseNumberAtReference = 3.3111;

    // Точные минимумы, взятые прямо с реальных предметов, при ReferenceScale (двуручное оружие +10).
    // Сюда добавляются новые характеристики по мере появления реальных примеров.
    private static readonly Dictionary<StatType, int> KnownMinAtReference = new()
    {
        [StatType.AttackPower] = 1326,
        [StatType.CriticalDamage] = 1474,
        [StatType.Health] = 7370,
        [StatType.CriticalDamageResistance] = 1474,
    };

    // scale — общий множитель вещи: зелёный множитель × (редкость) × руна.
    public static (int Min, int Max, int Value) GetRange(StatType statType, double scale)
    {
        var minAtReference = GetMinAtReference(statType);
        if (minAtReference <= 0 || scale <= 0)
            return (0, 0, 0);

        var min = (int)Math.Round(minAtReference * scale / ReferenceScale);
        var max = (int)Math.Round(min * MaxToMinRatio);
        var value = (int)Math.Round((min + max) / 2.0);

        return (min, max, value);
    }

    // Пересчитывает уже выбранные характеристики, когда у вещи меняется множитель (редкость или руна).
    // Если диапазон стата не правился вручную, он берётся заново из таблицы, а положение значения
    // внутри диапазона сохраняется. Если правился — все три числа масштабируются пропорционально.
    // Пустая и несобранная вручную комбинации возвращаются как есть.
    public static StatCombination Rescale(StatCombination combination, double oldScale, double newScale)
    {
        if (combination is not CustomStatCombination custom
            || oldScale <= 0
            || newScale <= 0
            || Math.Abs(oldScale - newScale) < 1e-9)
        {
            return combination;
        }

        var ratio = newScale / oldScale;
        var stats = new (StatType StatType, int Min, int Max, int Value)[custom.Stats.Length];

        for (var i = 0; i < stats.Length; i++)
        {
            var statType = custom.Stats[i].StatType;
            var min = custom.GetMin(i);
            var max = custom.GetMax(i);
            var value = custom.Stats[i].StatNumber;

            var oldRange = GetRange(statType, oldScale);
            if (oldRange.Max > 0 && oldRange.Min == min && oldRange.Max == max)
            {
                var newRange = GetRange(statType, newScale);
                var position = max > min ? Math.Clamp((value - min) / (double)(max - min), 0, 1) : 0.5;
                var newValue = RoundHalfUp(newRange.Min + position * (newRange.Max - newRange.Min));
                stats[i] = (statType, newRange.Min, newRange.Max, newValue);
            }
            else
            {
                stats[i] = (statType, RoundHalfUp(min * ratio), RoundHalfUp(max * ratio), RoundHalfUp(value * ratio));
            }
        }

        return new CustomStatCombination(custom.StatCombinationType, stats);
    }

    private static int RoundHalfUp(double number)
    {
        return (int)Math.Round(number, MidpointRounding.AwayFromZero);
    }

    private static double GetMinAtReference(StatType statType)
    {
        if (KnownMinAtReference.TryGetValue(statType, out var known))
            return known;

        if (statType == StatType.CharacterSpeed)
            return 0;

        var baseNumber = StatNumberProvider.StatTypeToBaseNumber(statType, StatCombinationType.ThreeStats);
        return baseNumber * MinPerBaseNumberAtReference;
    }
}
