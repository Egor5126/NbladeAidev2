using Models.Stats;

namespace Models.BaseModels;

// Числовые значения попадают в ссылку «Поделиться билдом» — не менять и не переставлять,
// новые элементы добавлять только в конец с новым номером.
public enum WarriorAura
{
    None = 0,
    Defense = 1,
    Resistance = 2,
}

// Ауры воина: активна одна из двух (или никакая). Бонус считается от итоговых значений
// после вещей, камней, эликсиров и усилений.
public static class WarriorAuraProvider
{
    public static string GetName(WarriorAura aura)
    {
        return aura switch
        {
            WarriorAura.None => "Без ауры",
            WarriorAura.Defense => "Аура защиты",
            WarriorAura.Resistance => "Аура стойкости",
            _ => throw new ArgumentOutOfRangeException(nameof(aura), aura, null)
        };
    }

    public static string GetDescription(WarriorAura aura)
    {
        return aura switch
        {
            WarriorAura.None => string.Empty,
            WarriorAura.Defense => "+11% брони и магической брони, +450 сопротивления крит. урону",
            WarriorAura.Resistance => "+12% сопротивления крит. урону",
            _ => throw new ArgumentOutOfRangeException(nameof(aura), aura, null)
        };
    }

    // Процентные прибавки: характеристика -> доля от текущего значения.
    public static (StatType StatType, double Percent)[] GetPercentBonuses(WarriorAura aura)
    {
        return aura switch
        {
            WarriorAura.None => Array.Empty<(StatType, double)>(),
            WarriorAura.Defense => new[] { (StatType.Armor, 0.11), (StatType.MagicArmor, 0.11) },
            WarriorAura.Resistance => new[] { (StatType.CriticalDamageResistance, 0.12) },
            _ => throw new ArgumentOutOfRangeException(nameof(aura), aura, null)
        };
    }

    // Фиксированные прибавки (прибавляются после процентных).
    public static (StatType StatType, int Value)[] GetFlatBonuses(WarriorAura aura)
    {
        return aura switch
        {
            WarriorAura.Defense => new[] { (StatType.CriticalDamageResistance, 450) },
            _ => Array.Empty<(StatType, int)>()
        };
    }
}
