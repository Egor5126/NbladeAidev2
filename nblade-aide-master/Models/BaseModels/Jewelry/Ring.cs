using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Ring : JewelryBase
{
    public Ring() : this(
        StatCombination.GetRandomStatCombination(),
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level12)
    {
    }

    public Ring(
        StatCombination statCombination,
        StuffRarity stuffRarity,
        JewelryRuneOfImprovementLevel jewelryRuneOfImprovementLevel)
        : base(statCombination, stuffRarity, jewelryRuneOfImprovementLevel, JewelryType.Ring)
    {
    }

    public override JewelryBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        JewelryRuneOfImprovementLevel? newRuneLevel = null,
        StuffRarity? newStuffRarity = null)
    {
        return new Ring(
            newStatCombination ?? StatCombination,
            newStuffRarity ?? StuffRarity,
            newRuneLevel ?? JewelryRuneOfImprovementLevel);
    }
}