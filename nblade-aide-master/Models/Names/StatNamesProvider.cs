using Models.Stats;

namespace Models.Names;

public static class StatNamesProvider
{
    public static string GetStatName(StatType statType)
    {
        return statType switch
        {
            StatType.AttackPower => "сила атаки",
            StatType.AttackSpeed => "скорость атаки",
            StatType.Accuracy => "точность",
            StatType.CriticalDamage => "критический урон",
            StatType.Armor => "броня",
            StatType.MagicArmor => "магическая броня",
            StatType.MagicalResistance => "сопр. магии",
            StatType.CriticalDamageResistance => "сопр. крит. урону",
            StatType.Evasion => "уклонение",
            StatType.Block => "блок",
            StatType.Parry => "парирование",
            StatType.CharacterSpeed => "скорость",
            StatType.Health => "здоровье",
            StatType.HealthRestoration => "восст. здоровья",
            StatType.Energy => "энергия",
            StatType.EnergyRestoration => "восст. энергии",
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }
}