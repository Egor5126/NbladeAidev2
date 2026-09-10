using Models.Stats;

namespace Models.BaseModels;

public class ConcentrationGem
{
    public ConcentrationGem(StatType? statType, ConcentrationStoneLevel gemLevel)
    {
        GemLevel = gemLevel;
        StatType = statType;
    }

    public StatType? StatType { get; }
    public ConcentrationStoneLevel GemLevel { get; }

    public ConcentrationGem CreateFromInstance(StatType? statType = null, ConcentrationStoneLevel? newGemLevel = null)
    {
        return new ConcentrationGem(
            statType ?? StatType,
            newGemLevel ?? GemLevel);
    }
}
