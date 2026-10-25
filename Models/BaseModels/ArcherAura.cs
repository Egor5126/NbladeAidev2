using Models.Stats;

namespace Models.BaseModels;

// Числовые значения попадают в ссылку «Поделиться билдом» — не менять и не переставлять,
// новые элементы добавлять только в конец с новым номером.
public enum ArcherAura
{
    None = 0,
    Accuracy = 1,
    AccuracyAttackPower = 2,
    AccuracyCriticalChance = 3,
    AccuracyCriticalDamage = 4,
}

// Аура точности лучника: точность +5% и, в зависимости от варианта, ещё одна характеристика +5%.
// Бонус считается от итоговых значений после вещей, камней, эликсиров и усилений.
public static class ArcherAuraProvider
{
    public static string GetName(ArcherAura aura)
    {
        return aura switch
        {
            ArcherAura.None => "Без ауры",
            ArcherAura.Accuracy => "Точность +5%",
            ArcherAura.AccuracyAttackPower => "Точность +5%, Сила атаки +5%",
            ArcherAura.AccuracyCriticalChance => "Точность +5%, Шанс крита +5%",
            ArcherAura.AccuracyCriticalDamage => "Точность +5%, Крит. урон +5%",
            _ => throw new ArgumentOutOfRangeException(nameof(aura), aura, null)
        };
    }

    // Процентные прибавки: характеристика -> доля от текущего значения.
    public static (StatType StatType, double Percent)[] GetPercentBonuses(ArcherAura aura)
    {
        return aura switch
        {
            ArcherAura.None => Array.Empty<(StatType, double)>(),
            ArcherAura.Accuracy => new[] { (StatType.Accuracy, 0.05) },
            ArcherAura.AccuracyAttackPower => new[] { (StatType.Accuracy, 0.05), (StatType.AttackPower, 0.05) },
            ArcherAura.AccuracyCriticalChance => new[] { (StatType.Accuracy, 0.05), (StatType.CriticalChance, 0.05) },
            ArcherAura.AccuracyCriticalDamage => new[] { (StatType.Accuracy, 0.05), (StatType.CriticalDamage, 0.05) },
            _ => throw new ArgumentOutOfRangeException(nameof(aura), aura, null)
        };
    }
}
