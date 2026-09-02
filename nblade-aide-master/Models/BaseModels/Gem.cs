using Models.Multipliers;
using Models.Stats;

namespace Models.BaseModels;

public class Gem
{
    public Gem(StatType statType, GemLevel gemLevel)
    {
        GemLevel = gemLevel;
        Stat = new Stat(statType, StatNumberProvider.GetGemStatNumber(statType, gemLevel));
        PotentialStats = GetPotentialStats(gemLevel);
    }

    public Stat Stat { get; }
    public GemLevel GemLevel { get; }
    public Stat[] PotentialStats { get; }

    public Gem CreateFromInstance(Stat? newStat = null, GemLevel? newGemLevel = null)
    {
        return new Gem(
            newStat?.StatType ?? Stat.StatType,
            newGemLevel ?? GemLevel);
    }

    private static Stat[] GetPotentialStats(GemLevel gemLevel)
    {
        return Enum.GetValues<StatType>()
            .Where(statType => statType != StatType.CharacterSpeed)
            .Select(statType => new Stat(statType, StatNumberProvider.GetGemStatNumber(statType, gemLevel)))
            .ToArray();
    }
}