namespace Models.Stats;

public abstract class StatCombination
{
    public abstract StatCombinationType StatCombinationType { get; }
    public abstract string Name { get; }
    public abstract Stat[] Stats { get; }
    public abstract Stat[] OriginStats { get; }

    public virtual StatCombination UseMultiplier(double multiplier)
    {
        foreach (var stat in Stats)
        {
            stat.UseMultiplier(multiplier);
        }

        return this;
    }

    public virtual StatCombination ResetMultipliers()
    {
        for (var i = 0; i < OriginStats.Length; ++i)
        {
            Stats[i] = new Stat(OriginStats[i].StatType, OriginStats[i].StatNumber);
        }

        return this;
    }

    public override string ToString()
    {
        var statsToString = string.Join<Stat>(", ", Stats);
        return $"{Name} ({statsToString})";
    }

    public static StatCombination GetRandomStatCombination(StatCombinationType type = StatCombinationType.ThreeStats)
    {
        var rand = new Random();
        var stats = type == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();
        var index = rand.Next(stats.Length);
        return stats[index];
    }
}