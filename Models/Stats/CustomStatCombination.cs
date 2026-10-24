namespace Models.Stats;

// Комбинация характеристик, собранная игроком вручную: он сам выбирает, какие
// именно статы стоят на предмете. Для каждой характеристики хранится диапазон
// (мин–макс), который виден на предмете в игре, и конкретное текущее значение
// в этом диапазоне — именно оно участвует в расчётах, как и в самой игре.
// В отличие от именованных комбинаций из StatCombinationsList, значения здесь
// никогда не пересчитываются множителями руны/редкости — что игрок ввёл,
// то и остаётся, при любом пересоздании вещи.
public sealed class CustomStatCombination : StatCombination
{
    private readonly Stat[] _stats;
    private readonly (int Min, int Max)[] _ranges;

    public CustomStatCombination(StatCombinationType statCombinationType, IEnumerable<(StatType StatType, int Min, int Max, int Value)> stats)
    {
        StatCombinationType = statCombinationType;
        var list = stats.ToArray();
        _stats = list.Select(s => new Stat(s.StatType, s.Value)).ToArray();
        _ranges = list.Select(s => (s.Min, s.Max)).ToArray();
    }

    public override StatCombinationType StatCombinationType { get; }
    public override string Name => string.Empty;
    public override Stat[] Stats => _stats;
    public override Stat[] OriginStats => _stats;

    public int GetMin(int index) => _ranges[index].Min;
    public int GetMax(int index) => _ranges[index].Max;

    // Ручные значения не должны меняться множителями руны/редкости — какое
    // значение ввёл игрок, такое и должно остаться.
    public override StatCombination UseMultiplier(double multiplier) => this;
    public override StatCombination ResetMultipliers() => this;

    public override string ToString() => string.Empty;
}
