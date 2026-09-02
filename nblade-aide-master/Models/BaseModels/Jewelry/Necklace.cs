using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Necklace : JewelryBase
{
    public Necklace() : this(
        StatCombination.GetRandomStatCombination(),
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level12)
    {
    }

    public Necklace(
        StatCombination statCombination,
        StuffRarity stuffRarity,
        JewelryRuneOfImprovementLevel jewelryRuneOfImprovementLevel)
        : base(statCombination, stuffRarity, jewelryRuneOfImprovementLevel, JewelryType.Necklace)
    {
    }

    public override JewelryBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        JewelryRuneOfImprovementLevel? newRuneLevel = null,
        StuffRarity? newStuffRarity = null)
    {
        return new Necklace(
            newStatCombination ?? StatCombination,
            newStuffRarity ?? StuffRarity,
            newRuneLevel ?? JewelryRuneOfImprovementLevel);
    }
}