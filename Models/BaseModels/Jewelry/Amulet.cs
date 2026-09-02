using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Amulet : JewelryBase
{
    public Amulet() : this(
        StatCombination.GetRandomStatCombination(),
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level12)
    {
    }

    public Amulet(
        StatCombination statCombination,
        StuffRarity stuffRarity,
        JewelryRuneOfImprovementLevel jewelryRuneOfImprovementLevel)
        : base(statCombination, stuffRarity, jewelryRuneOfImprovementLevel, JewelryType.Amulet)
    {
    }

    public override JewelryBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        JewelryRuneOfImprovementLevel? newRuneLevel = null,
        StuffRarity? newStuffRarity = null)
    {
        return new Amulet(
            newStatCombination ?? StatCombination,
            newStuffRarity ?? StuffRarity,
            newRuneLevel ?? JewelryRuneOfImprovementLevel);
    }
}