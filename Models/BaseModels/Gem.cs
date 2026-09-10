using Models.Multipliers;
using Models.Stats;

namespace Models.BaseModels;

public class Gem
{
    public Gem(StatType? statType, PowerStoneLevel gemLevel)
    {
        GemLevel = gemLevel;
        StatType = statType;
        Stat = statType.HasValue
            ? new Stat(statType.Value, StatNumberProvider.GetGemStatNumber(statType.Value, gemLevel))
            : null;
        PotentialStats = GetPotentialStats(gemLevel);
    }

    public StatType? StatType { get; }
    public Stat? Stat { get; }
    public PowerStoneLevel GemLevel { get; }
    public Stat[] PotentialStats { get; }

    public Gem CreateFromInstance(Stat? newStat = null, PowerStoneLevel? newGemLevel = null)
    {
        return new Gem(
            newStat?.StatType ?? StatType,
            newGemLevel ?? GemLevel);
    }

    private static Stat[] GetPotentialStats(PowerStoneLevel gemLevel)
    {
        return Enum.GetValues<Models.Stats.StatType>()
            .Where(statType => statType != Models.Stats.StatType.CharacterSpeed)
            .Select(statType => new Stat(statType, StatNumberProvider.GetGemStatNumber(statType, gemLevel)))
            .ToArray();
    }
}