using Models.Multipliers;
using Models.Names;
using Models.Stats;

namespace Models.BaseModels.Cloak;

public class Cloak
{
    public Cloak() : this(
        StatCombination.GetRandomStatCombination(),
        StuffRarity.Orange,
        StuffRuneOfImprovementLevel.Level10) {}

    public Cloak(
        StatCombination statCombination,
        StuffRarity stuffRarity,
        StuffRuneOfImprovementLevel runeOfImprovementLevel)
    {
        var runeMultiplier = StuffRuneOfImprovementMultiplier.RuneMultiplier(runeOfImprovementLevel);
        var whiteStats = StatNumberProvider.GetWhiteStatCloakNumbers(stuffRarity);
        var greenMultiplier = BaseStuffStatsMultiplier.GetCloakGreenStatMultiplier();
        var stuffRarityMultiplier = BaseStuffStatsMultiplier.GetStuffRarityMultiplier(stuffRarity);

        StuffRarity = stuffRarity;
        BaseArmorStat = new Stat(StatType.Armor, whiteStats.Armor)
            .UseMultiplier(runeMultiplier);
        BaseMagicArmorStat = new Stat(StatType.MagicArmor, whiteStats.MagicArmor)
            .UseMultiplier(runeMultiplier);
        RuneOfImprovementLevel = runeOfImprovementLevel;
        StatCombination = statCombination
            .ResetMultipliers()
            .UseMultiplier(greenMultiplier)
            .UseMultiplier(stuffRarityMultiplier)
            .UseMultiplier(runeMultiplier);
        Name = CloakNamesProvider.GetCloakName()
               + " " + statCombination.Name
               + (runeOfImprovementLevel != StuffRuneOfImprovementLevel.None
                   ? $" +{(int)runeOfImprovementLevel}"
                   : "");
        SimpleName = CloakNamesProvider.GetCloakSimpleName();
        PotentialStatCombinations = GetPotentialStatCombinations(
            statCombination.StatCombinationType,
            greenMultiplier,
            stuffRarityMultiplier,
            runeMultiplier);
    }

    public StuffRarity StuffRarity { get; }
    public Stat BaseArmorStat { get; }
    public Stat BaseMagicArmorStat { get; }
    public StuffRuneOfImprovementLevel RuneOfImprovementLevel { get; }
    public StatCombination StatCombination { get; }
    public string Name { get; }
    public string SimpleName { get; }
    public StatCombination[] PotentialStatCombinations { get; }

    public Stat[] GetStats()
    {
        return new[] { BaseArmorStat, BaseMagicArmorStat }.Concat(StatCombination.Stats).ToArray();
    }

    public Cloak CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        StuffRarity? newStuffRarity = null)
    {
        return new Cloak(
            newStatCombination ?? StatCombination,
            newStuffRarity ?? StuffRarity,
            newRuneLevel ?? RuneOfImprovementLevel);
    }

    private StatCombination[] GetPotentialStatCombinations(
        StatCombinationType statCombinationType,
        double greenMultiplier,
        double stuffRarityMultiplier,
        double runeMultiplier)
    {
        var statCombinations = statCombinationType == StatCombinationType.ThreeStats 
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();
        foreach (var statCombination in statCombinations)
        {
            statCombination
                .UseMultiplier(greenMultiplier)
                .UseMultiplier(stuffRarityMultiplier)
                .UseMultiplier(runeMultiplier);
        }

        return statCombinations;
    }
}