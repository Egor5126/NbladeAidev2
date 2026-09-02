using Models.Multipliers;
using Models.Stats;

namespace Models.StatCombinations;

public class ThreeStatCombination : StatCombination
{
    public ThreeStatCombination(string name, StatType mainStat, StatType secondaryStat, StatType threeStagedStat)
    {
        const StatCombinationType statCombinationType = StatCombinationType.ThreeStats;
        StatCombinationType = statCombinationType;
        Name = name;
        Stats = CreateStats(mainStat, secondaryStat, threeStagedStat, statCombinationType);
        OriginStats = CreateStats(mainStat, secondaryStat, threeStagedStat, statCombinationType);
    }

    public override StatCombinationType StatCombinationType { get; }
    public override string Name { get; }
    public override Stat[] Stats { get; }
    public override Stat[] OriginStats { get; }

    private static Stat[] CreateStats(
        StatType mainStat,
        StatType secondaryStat,
        StatType threeStagedStat,
        StatCombinationType statCombinationType)
    {
        return new[]
        {
            new Stat(mainStat, statCombinationType)
                .UseMultiplier(StatMultiplier.ThreeStatMultiplier(1)),
            new Stat(secondaryStat, statCombinationType)
                .UseMultiplier(StatMultiplier.ThreeStatMultiplier(2)),
            new Stat(threeStagedStat, statCombinationType)
                .UseMultiplier(StatMultiplier.ThreeStatMultiplier(3)),
        };
    }
}