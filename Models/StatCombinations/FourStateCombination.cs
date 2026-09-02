using Models.Multipliers;
using Models.Stats;

namespace Models.StatCombinations;

public class FourStateCombination : StatCombination
{
    public FourStateCombination(
        string name,
        StatType mainStat,
        StatType secondaryStat,
        StatType threeStagedStat,
        StatType fourStagedStat)
    {
        const StatCombinationType statCombinationType = StatCombinationType.FourStats;
        StatCombinationType = statCombinationType;
        Name = name;

        Stats = CreateStats(mainStat, secondaryStat, threeStagedStat, fourStagedStat, statCombinationType, name);
        OriginStats = CreateStats(mainStat, secondaryStat, threeStagedStat, fourStagedStat, statCombinationType, name);
    }

    public override StatCombinationType StatCombinationType { get; }
    public override string Name { get; }
    public override Stat[] Stats { get; }
    public override Stat[] OriginStats { get; }

    private static Stat[] CreateStats(
        StatType mainStat,
        StatType secondaryStat,
        StatType threeStagedStat,
        StatType fourStagedStat,
        StatCombinationType statCombinationType,
        string name)
    {
        Stat? fourStat = null;
        if (name == "устойчивости" && fourStagedStat == StatType.CriticalDamageResistance)
        {
            fourStat = new Stat(fourStagedStat, 86);
        }

        return new[]
        {
            new Stat(mainStat, statCombinationType)
                .UseMultiplier(StatMultiplier.FourStatMultiplier(1)),
            new Stat(secondaryStat, statCombinationType)
                .UseMultiplier(StatMultiplier.FourStatMultiplier(2)),
            new Stat(threeStagedStat, statCombinationType)
                .UseMultiplier(StatMultiplier.FourStatMultiplier(3)),
            fourStat ?? new Stat(fourStagedStat, statCombinationType)
                .UseMultiplier(StatMultiplier.FourStatMultiplier(4)),
        };
    }
}