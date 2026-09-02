using Models.Multipliers;
using Models.Names;
using Models.Stats;

namespace Models.BaseModels.Armors;

public abstract class ArmorBase
{
    protected ArmorBase(CharacterClass characterClass, ArmorType armorType) : this(
        StatCombination.GetRandomStatCombination(),
        characterClass,
        armorType,
        StuffRuneOfImprovementLevel.Level12,
        new Gem(StatType.Accuracy, GemLevel.None),
        new Gem(StatType.AttackPower, GemLevel.None))
    {
    }

    protected ArmorBase(
        StatCombination statCombination,
        CharacterClass characterClass,
        ArmorType armorType,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        Gem gem2)
    {
        var whiteStats = StatNumberProvider.GetWhiteStatArmorNumbers(characterClass);
        var whiteArmorMultiplier = BaseStuffStatsMultiplier.GetArmorWhiteStatMultiplier(armorType);
        var runeMultiplier = StuffRuneOfImprovementMultiplier.RuneMultiplier(runeOfImprovementLevel);
        var greenMultiplier = BaseStuffStatsMultiplier.GetArmorGreenStatMultiplier(armorType);

        CharacterClass = characterClass;
        ArmorType = armorType;
        RuneOfImprovementLevel = runeOfImprovementLevel;
        Gem1 = gem1;
        Gem2 = gem2;
        BaseArmorStat = new Stat(StatType.Armor, whiteStats.Armor)
            .UseMultiplier(whiteArmorMultiplier)
            .UseMultiplier(runeMultiplier);
        BaseMagicArmorStat = new Stat(StatType.MagicArmor, whiteStats.MagicArmor)
            .UseMultiplier(whiteArmorMultiplier)
            .UseMultiplier(runeMultiplier);
        StatCombination = statCombination
            .ResetMultipliers()
            .UseMultiplier(greenMultiplier)
            .UseMultiplier(runeMultiplier);
        Name = GetArmorName(armorType, statCombination, characterClass, runeOfImprovementLevel);
        SimpleName = ArmorNamesProvider.GetArmorSimpleName(armorType);
        PotentialStatCombinations = GetPotentialStatCombinations(statCombination.StatCombinationType, greenMultiplier, runeMultiplier);
    }

    public ArmorType ArmorType { get; }
    public StuffRuneOfImprovementLevel RuneOfImprovementLevel { get; }
    public Stat BaseArmorStat { get; }
    public Stat BaseMagicArmorStat { get; }
    public StatCombination StatCombination { get; private set; }
    public CharacterClass CharacterClass { get; }
    public string Name { get; private set; }
    public string SimpleName { get; set; }
    public Gem Gem1 { get; set; }
    public Gem Gem2 { get; set; }
    public StatCombination[] PotentialStatCombinations { get; }

    public abstract ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null);

    private StatCombination[] GetPotentialStatCombinations(
        StatCombinationType statCombinationType,
        double greenMultiplier, 
        double runeMultiplier)
    {
        var statCombinations = statCombinationType == StatCombinationType.ThreeStats 
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();
        foreach (var statCombination in statCombinations)
        {
            statCombination
                .UseMultiplier(greenMultiplier)
                .UseMultiplier(runeMultiplier);
        }

        return statCombinations;
    }

    private string GetArmorName(
        ArmorType armorType,
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel)
    {
        return ArmorNamesProvider.GetArmorName(armorType, statCombination.StatCombinationType, characterClass)
               + " " + statCombination.Name
               + (runeOfImprovementLevel != StuffRuneOfImprovementLevel.None
                   ? $" +{(int)runeOfImprovementLevel}"
                   : "");
    }

    public Stat[] GetMultipliedStats()
    {
        return new[] { BaseArmorStat, BaseMagicArmorStat }.Concat(StatCombination.Stats).ToArray();
    }

    public Stat[] GetNonMultipliedStats()
    {
        return new[] { Gem1.Stat, Gem2.Stat };
    }

    public void UpdateStatCombination(StatCombination newStatCombination)
    {
        StatCombination = newStatCombination;
        Name = GetArmorName(ArmorType, newStatCombination, CharacterClass, RuneOfImprovementLevel);
    }
}