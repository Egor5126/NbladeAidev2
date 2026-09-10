using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Amulet : JewelryBase
{
    public Amulet() : this(
        // Вещи с 3 характеристиками отключены (кроме плаща) — новая бижутерия всегда создаётся с 4 характеристиками
        StatCombination.GetRandomStatCombination(StatCombinationType.FourStats),
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level10)
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