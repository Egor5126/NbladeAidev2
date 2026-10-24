using Models.Stats;

namespace Models.BaseModels.Amplification;

public class UniversalSoldiers : Amplification
{
    public UniversalSoldiers() : base(
        "Универсальные солдаты (гильдия)",
        new []
        {
            (StatType.AttackPower, 1.03),
            (StatType.AttackSpeed, 1.03),
            (StatType.Accuracy, 1.03),
            (StatType.CriticalChance, 1.03),
            (StatType.CriticalDamage, 1.03),
            (StatType.Armor, 1.03),
            (StatType.MagicArmor, 1.03),
            (StatType.MagicalResistance, 1.03),
            (StatType.CriticalDamageResistance, 1.03),
            (StatType.Evasion, 1.03),
            (StatType.Block, 1.03),
            (StatType.Parry, 1.03),
            (StatType.Health, 1.03),
            (StatType.HealthRestoration, 1.03),
            (StatType.Energy, 1.03),
            (StatType.EnergyRestoration, 1.03),
        },
        "Все ваши характеристики увеличены на 3%")
    {
    }
}