using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Necklace : JewelryBase
{
    public Necklace() : this(
        // Вещи с 3 характеристиками отключены (кроме плаща) — новая бижутерия всегда создаётся с 4 характеристиками
        StatCombination.GetRandomStatCombination(StatCombinationType.FourStats),
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level10)
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