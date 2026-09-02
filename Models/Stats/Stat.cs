using Models.Multipliers;
using Models.Names;
using Models.StatCombinations;

namespace Models.Stats;

public class Stat
{
    public Stat(StatType statType, StatCombinationType statCombinationType)
    {
        StatType = statType;
        StatNumber = StatNumberProvider.StatTypeToBaseNumber(StatType, statCombinationType);
    }

    public Stat(StatType statType, int statNumber)
    {
        StatType = statType;
        StatNumber = statNumber;
    }

    public StatType StatType { get; set; }
    public int StatNumber { get; set; }

    public Stat UseMultiplier(double baseMultiplier)
    {
        var multipliedResult = StatNumber * baseMultiplier;
        if (baseMultiplier < 0)
            StatNumber = (int)Math.Floor(multipliedResult);
        else 
            StatNumber = (int)Math.Ceiling(multipliedResult);
        return this;
    }

    public override string ToString()
    {
        return $"{StatNamesProvider.GetStatName(StatType)}: +{StatNumber}";
    }
}