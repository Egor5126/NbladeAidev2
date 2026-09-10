using Models.Multipliers;
using Models.Names;
using Models.Stats;

namespace Models.BaseModels.Weapon;

public abstract class WeaponBase
{
    protected WeaponBase(
        StatCombination statCombination,
        CharacterClass characterClass,
        WeaponHandedClass weaponHandedClass,
        WeaponType weaponType,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        WeaponOrder weaponOrder,
        List<WeaponGem>? weaponGems)
    {
        var runeMultiplier = StuffRuneOfImprovementMultiplier.RuneMultiplier(runeOfImprovementLevel);
        var greenMultiplier = BaseStuffStatsMultiplier.GetWeaponGreenStatMultiplier(weaponHandedClass);

        WeaponType = weaponType;
        WeaponOrder = weaponOrder;
        CharacterClass = characterClass;
        WeaponHandedClass = weaponHandedClass;
        RuneOfImprovementLevel = runeOfImprovementLevel;
        StatCombination = statCombination
            .ResetMultipliers()
            .UseMultiplier(greenMultiplier)
            .UseMultiplier(runeMultiplier);
        Name = WeaponNamesProvider.GetWeaponName(weaponType, statCombination.StatCombinationType)
               + " " + statCombination.Name
               + (runeOfImprovementLevel != StuffRuneOfImprovementLevel.None
                   ? $" +{(int)runeOfImprovementLevel}"
                   : "");
        SimpleName = WeaponNamesProvider.GetWeaponSimpleName(weaponOrder);
        // TODO
        BaseStat = weaponType == WeaponType.Shield
            ? new Stat(StatType.Block, 3056).UseMultiplier(runeMultiplier)
            : null;
        PotentialStatCombinations = GetPotentialStatCombinations(statCombination.StatCombinationType, greenMultiplier, runeMultiplier);

        var gemNumber = WeaponHandedClass == WeaponHandedClass.OneHandedWeapon ? 3 : 6;
        WeaponGems = weaponGems != null
            ? weaponGems.Take(gemNumber).ToList()
            : new List<WeaponGem>();
        for (var i = 0; i < gemNumber && WeaponGems.Count < gemNumber; i++)
        {
            WeaponGems.Add(new WeaponGem((StatType)i, GemLevel.None));
        }
    }

    public WeaponOrder WeaponOrder { get; }
    public WeaponHandedClass WeaponHandedClass { get; }
    public WeaponType WeaponType { get; }
    public StuffRuneOfImprovementLevel RuneOfImprovementLevel { get; }
    public Stat? BaseStat { get; }
    public StatCombination StatCombination { get; }
    public CharacterClass CharacterClass { get; }
    public string Name { get; }
    public string SimpleName { get; set; }
    public StatCombination[] PotentialStatCombinations { get; }
    public List<WeaponGem> WeaponGems { get; }

    public abstract WeaponBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null);

    private StatCombination[] GetPotentialStatCombinations(
        StatCombinationType statCombinationType,
        double greenMultiplier, 
        double runeMultiplier)
    {
        // Вещи с 3 характеристиками отключены (кроме плаща) — оставляем только вариант с 4 характеристиками
        var statCombinations = /*statCombinationType == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : */StatCombinationsList.FourStateCombinations();
        foreach (var statCombination in statCombinations)
        {
            statCombination
                .UseMultiplier(greenMultiplier)
                .UseMultiplier(runeMultiplier);
        }

        return statCombinations;
    }

    public Stat[] GetStats()
    {
        return BaseStat == null
            ? StatCombination.Stats
            : new[] { BaseStat }.Concat(StatCombination.Stats).ToArray();
    }

    public WeaponType[] GetAvailableWeaponTypes()
    {
        return WeaponOrder == WeaponOrder.Main
            ? GetAvailableMainWeaponTypes()
            : GetAvailableSecondWeaponTypes();
    }

    private WeaponType[] GetAvailableMainWeaponTypes()
    {
        return CharacterClass switch
        {
            CharacterClass.Warrior => new []
            {
                WeaponType.Axe,
                WeaponType.Blade,
                WeaponType.Flamberge,
                WeaponType.Halberd,
            },
            CharacterClass.Bandit => new []
            {
                WeaponType.Dagger,
                WeaponType.Axe,
                WeaponType.Blade,
            },
            CharacterClass.Priest => new []
            {
                WeaponType.Mint,
                WeaponType.Hammer,
            },
            CharacterClass.Archer => new []
            {
                WeaponType.Bow,
                WeaponType.Crossbow,
            },
            CharacterClass.Wizard => new []
            {
                WeaponType.Staff,
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private WeaponType[] GetAvailableSecondWeaponTypes()
    {
        return CharacterClass switch
        {
            CharacterClass.Warrior => new []
            {
                WeaponType.Axe,
                WeaponType.Blade,
                WeaponType.Shield,
            },
            CharacterClass.Bandit => new []
            {
                WeaponType.Dagger,
                WeaponType.Axe,
                WeaponType.Blade,
            },
            CharacterClass.Priest => new []
            {
                WeaponType.Mint,
                WeaponType.Shield,
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public WeaponBase CreateFromWeaponType(WeaponType weaponType)
    {
        return weaponType switch
        {
            WeaponType.Shield => new Shield(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Blade => new Blade(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Flamberge => new Flamberge(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Halberd => new Halberd(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Staff => new Staff(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Bow => new Bow(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Crossbow => new Crossbow(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Dagger => new Dagger(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Axe => new Axe(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Mint => new Mint(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            WeaponType.Hammer => new Hammer(StatCombination, CharacterClass, RuneOfImprovementLevel, WeaponOrder, WeaponGems),
            _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
        };
    }
}