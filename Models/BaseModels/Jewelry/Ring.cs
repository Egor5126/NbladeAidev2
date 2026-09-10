using Models.Stats;

namespace Models.BaseModels.Jewelry;

public class Ring : JewelryBase
{
    public Ring() : this(
        // Вещи с 3 характеристиками отключены (кроме плаща) — новая бижутерия всегда создаётся с 4 характеристиками.
        // Конкретная комбинация характеристик изначально не выбрана — её должен выбрать игрок.
        EmptyStatCombination.FourStats,
        StuffRarity.Orange,
        JewelryRuneOfImprovementLevel.Level10)
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