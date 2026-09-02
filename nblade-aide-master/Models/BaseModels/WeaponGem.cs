using Models.Stats;

namespace Models.BaseModels;

public class WeaponGem
{
    public WeaponGem(StatType statType, GemLevel gemLevel)
    {
        GemLevel = gemLevel;
        StatType = statType;
    }

    public StatType StatType { get; }
    public GemLevel GemLevel { get; }

    public WeaponGem CreateFromInstance(StatType? statType = null, GemLevel? newGemLevel = null)
    {
        return new WeaponGem(
            statType ?? StatType,
            newGemLevel ?? GemLevel);
    }
}