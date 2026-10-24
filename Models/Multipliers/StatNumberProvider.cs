using Models.BaseModels;
using Models.Stats;

namespace Models.Multipliers;

public static class StatNumberProvider
{
    public static int StatTypeToBaseNumber(StatType statType, StatCombinationType statCombinationType)
    {
        return statCombinationType switch
        {
            StatCombinationType.ThreeStats => ThreeStatTypeToBaseNumber(statType),
            StatCombinationType.FourStats => FourStatTypeToBaseNumber(statType),
            _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
        };
    }

    public static int GetGemStatNumber(StatType statType, PowerStoneLevel gemLevel)
    {
        return PowerStoneStatNumber(statType, gemLevel);
    }

    public static double GetWeaponGemMultiplier(GemLevel gemLevel)
    {
        return gemLevel switch
        {
            GemLevel.None => 0,
            GemLevel.Level1 => 0.0175,
            GemLevel.Level2 => 0.0245,
            GemLevel.Level3 => 0.035,
            GemLevel.Level4 => 0.049,
            GemLevel.Level5 => 0.07,
            _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
        };
    }

    public static double GetConcentrationStoneMultiplier(ConcentrationStoneLevel gemLevel)
    {
        return gemLevel switch
        {
            ConcentrationStoneLevel.None => 0,
            ConcentrationStoneLevel.Level6 => 0.0217,
            ConcentrationStoneLevel.Level10 => 0.05,
            _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
        };
    }

    public static (int Armor, int MagicArmor) GetWhiteStatArmorNumbers(CharacterClass characterClass)
    {
        return WhiteArmorStatNumbers(characterClass);
    }

    public static (int Armor, int MagicArmor) GetWhiteStatCloakNumbers(StuffRarity stuffRarity)
    {
        return stuffRarity switch
        {
            StuffRarity.Violet => (5972, 5972),
            StuffRarity.Orange => (7588, 7588),
            StuffRarity.Red => (9204, 9204),
            _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
        };
    }

    public static (int Armor, int MagicArmor) GetWhiteStatJewelryNumbers(
        StuffRarity stuffRarity,
        JewelryRuneOfImprovementLevel runeLevel)
    {
        if (stuffRarity != StuffRarity.Orange)
            return (0, 0);

        const int baseValue = 1268;
        var runeMultiplier = JewelryRuneOfImprovementMultiplier.RuneMultiplier(runeLevel);
        var value = (int)Math.Round(baseValue * runeMultiplier);

        return (value, value);
    }

    private static int ThreeStatTypeToBaseNumber(StatType statType)
    {
        return statType switch
        {
            StatType.AttackPower => 400,
            StatType.AttackSpeed => 445,
            StatType.Accuracy => 445,
            StatType.CriticalChance => 445,
            StatType.CriticalDamage => 445,
            StatType.Armor => 4785,
            StatType.MagicArmor => 4785,
            StatType.MagicalResistance => 1157,
            StatType.CriticalDamageResistance => 445,
            StatType.Evasion => 1157,
            StatType.Block => 445,
            StatType.Parry => 1157,
            StatType.CharacterSpeed => 0,
            StatType.Health => 2226,
            StatType.HealthRestoration => 512,
            StatType.Energy => 1335,
            StatType.EnergyRestoration => 179,
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }

    private static int FourStatTypeToBaseNumber(StatType statType)
    {
        return statType switch
        {
            StatType.AttackPower => 300,
            StatType.AttackSpeed => 333,
            StatType.Accuracy => 333,
            StatType.CriticalChance => 333,
            StatType.CriticalDamage => 333,
            StatType.Armor => 3589,
            StatType.MagicArmor => 3589,
            StatType.MagicalResistance => 868,
            StatType.CriticalDamageResistance => 333,
            StatType.Evasion => 868,
            StatType.Block => 333,
            StatType.Parry => 868,
            StatType.CharacterSpeed => 0,
            StatType.Health => 1669,
            StatType.HealthRestoration => 384,
            StatType.Energy => 1000,
            StatType.EnergyRestoration => 134,
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }

    private static (int armor, int magicArmor) WhiteArmorStatNumbers(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => (2389, 933),
            CharacterClass.Bandit => (1493, 1493),
            CharacterClass.Priest => (1866, 1190),
            CharacterClass.Archer => (1493, 1493),
            CharacterClass.Wizard => (933, 2389),
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }

    private static int PowerStoneStatNumber(StatType statType, PowerStoneLevel gemLevel)
    {
        return statType switch
        {
            StatType.AttackPower => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 180,
                PowerStoneLevel.Level10 => 418,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.AttackSpeed => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Accuracy => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.CriticalChance => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.CriticalDamage => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Armor => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 2160,
                PowerStoneLevel.Level10 => 4986,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.MagicArmor => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 2160,
                PowerStoneLevel.Level10 => 4986,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.MagicalResistance => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 520,
                PowerStoneLevel.Level10 => 1206,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.CriticalDamageResistance => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Evasion => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 520,
                PowerStoneLevel.Level10 => 1206,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Block => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 215,
                PowerStoneLevel.Level10 => 464,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Parry => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 520,
                PowerStoneLevel.Level10 => 1206,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Health => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 1005,
                PowerStoneLevel.Level10 => 2320,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.HealthRestoration => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 230,
                PowerStoneLevel.Level10 => 534,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.Energy => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 605,
                PowerStoneLevel.Level10 => 1392,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.EnergyRestoration => gemLevel switch
            {
                PowerStoneLevel.None => 0,
                PowerStoneLevel.Level6 => 81,
                PowerStoneLevel.Level10 => 188,
                _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
            },
            StatType.CharacterSpeed => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
        };
    }

    // TODO
    public static Stat[] GetBaseStats(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Wizard => new []
            {
                new Stat(StatType.AttackPower, 5902),
                new Stat(StatType.Accuracy, 6746),
                new Stat(StatType.CriticalChance, 5340),
                new Stat(StatType.MagicalResistance, 6746),
                new Stat(StatType.CriticalDamageResistance, 4778),
                new Stat(StatType.Evasion, 4778),
                new Stat(StatType.Parry, 4497),
                new Stat(StatType.CharacterSpeed, 100),
                new Stat(StatType.Health, 23890),
                new Stat(StatType.HealthRestoration, 5621),
                new Stat(StatType.Energy, 21923),
                new Stat(StatType.EnergyRestoration, 2459),
            },
            CharacterClass.Warrior => new []
            {
                new Stat(StatType.AttackPower, 5621),
                new Stat(StatType.Accuracy, 4683),
                new Stat(StatType.CriticalChance, 4683),
                new Stat(StatType.MagicalResistance, 4683),
                new Stat(StatType.CriticalDamageResistance, 7027),
                new Stat(StatType.Evasion, 5621),
                new Stat(StatType.Parry, 6746),
                new Stat(StatType.CharacterSpeed, 100),
                new Stat(StatType.Health, 37926),
                new Stat(StatType.HealthRestoration, 6746),
                new Stat(StatType.Energy, 12985),
                new Stat(StatType.EnergyRestoration, 1790),
            },
            CharacterClass.Bandit => new []
            {
                new Stat(StatType.AttackPower, 6746),
                new Stat(StatType.Accuracy, 5340),
                new Stat(StatType.CriticalChance, 7308),
                new Stat(StatType.MagicalResistance, 4778),
                new Stat(StatType.CriticalDamageResistance, 5621),
                new Stat(StatType.Evasion, 7308),
                new Stat(StatType.Parry, 6430),
                new Stat(StatType.CharacterSpeed, 100),
                new Stat(StatType.Health, 25577),
                new Stat(StatType.HealthRestoration, 4328),
                new Stat(StatType.Energy, 14048),
                new Stat(StatType.EnergyRestoration, 1967),
            },
            CharacterClass.Priest => new []
            {
                new Stat(StatType.AttackPower, 5115),
                new Stat(StatType.Accuracy, 4497),
                new Stat(StatType.CriticalChance, 5059),
                new Stat(StatType.MagicalResistance, 7027),
                new Stat(StatType.CriticalDamageResistance, 6183),
                new Stat(StatType.Evasion, 5115),
                new Stat(StatType.Parry, 5621),
                new Stat(StatType.CharacterSpeed, 100),
                new Stat(StatType.Health, 30917),
                new Stat(StatType.HealthRestoration, 5902),
                new Stat(StatType.Energy, 18550),
                new Stat(StatType.EnergyRestoration, 2164),
            },
            CharacterClass.Archer => new []
            {
                new Stat(StatType.AttackPower, 5733),
                new Stat(StatType.Accuracy, 6736),
                new Stat(StatType.CriticalChance, 7026),
                new Stat(StatType.MagicalResistance, 5621),
                new Stat(StatType.CriticalDamageResistance, 4946),
                new Stat(StatType.Evasion, 6858),
                new Stat(StatType.Parry, 5059),
                new Stat(StatType.CharacterSpeed, 100),
                new Stat(StatType.Health, 24170),
                new Stat(StatType.HealthRestoration, 5059),
                new Stat(StatType.Energy, 16021),
                new Stat(StatType.EnergyRestoration, 2066),
            },
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }
}