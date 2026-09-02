using Models.Multipliers;
using Models.Names;
using Models.Stats;

namespace Models.BaseModels.Jewelry;

public abstract class JewelryBase
{
    protected JewelryBase(
        StatCombination statCombination,
        StuffRarity stuffRarity,
        JewelryRuneOfImprovementLevel jewelryRuneOfImprovementLevel,
        JewelryType jewelryType)
    {
        var runeMultiplier = JewelryRuneOfImprovementMultiplier.RuneMultiplier(jewelryRuneOfImprovementLevel);
        var whiteStats = StatNumberProvider.GetWhiteStatJewelryNumbers(stuffRarity, jewelryRuneOfImprovementLevel);
        var greenMultiplier = BaseStuffStatsMultiplier.GetJewelryGreenStatMultiplier();
        var stuffRarityMultiplier = BaseStuffStatsMultiplier.GetStuffRarityMultiplier(stuffRarity);

        StuffRarity = stuffRarity;
        BaseArmorStat = new Stat(StatType.Armor, whiteStats.Armor);
        BaseMagicArmorStat = new Stat(StatType.MagicArmor, whiteStats.MagicArmor);
        JewelryRuneOfImprovementLevel = jewelryRuneOfImprovementLevel;
        StatCombination = statCombination
            .ResetMultipliers()
            .UseMultiplier(greenMultiplier)
            .UseMultiplier(stuffRarityMultiplier)
            .UseMultiplier(runeMultiplier);
        Name = JewelryNamesProvider.GetJewelryName(jewelryType, stuffRarity, statCombination.StatCombinationType)
               + " " + statCombination.Name
               + (jewelryRuneOfImprovementLevel != JewelryRuneOfImprovementLevel.None
                   ? $" +{(int)jewelryRuneOfImprovementLevel}"
                   : "");
        SimpleName = JewelryNamesProvider.GetJewelrySimpleName(jewelryType);
        PotentialStatCombinations = GetPotentialStatCombinations(
            statCombination.StatCombinationType,
            greenMultiplier,
            stuffRarityMultiplier,
            runeMultiplier);
    }

    public StuffRarity StuffRarity { get; }
    public Stat BaseArmorStat { get; }
    public Stat BaseMagicArmorStat { get; }
    public JewelryRuneOfImprovementLevel JewelryRuneOfImprovementLevel { get; }
    public StatCombination StatCombination { get; }
    public string Name { get; }
    public string SimpleName { get; }
    public StatCombination[] PotentialStatCombinations { get; }

    public abstract JewelryBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        JewelryRuneOfImprovementLevel? newRuneLevel = null,
        StuffRarity? newStuffRarity = null);

    public Stat[] GetMultipliedStats()
    {
        return StatCombination.Stats;
    }

    public Stat[] GetNonMultipliedStats()
    {
        return new[] { BaseArmorStat, BaseMagicArmorStat };
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