using Models.Stats;

namespace Models.TalentsModels;

public class TalentBranch
{
    public TalentBranch(string name, (StatType StatType, double multiplier)[] statTypesToIncrease)
    {
        Name = name;
        StatTypesToIncrease = statTypesToIncrease;
        InvestedPoints = 0;
    }

    public int MaxBranchPoints { get; } = 24;
    public string Name { get; set; }
    public (StatType StatType, double BaseMultiplier)[] StatTypesToIncrease { get; set; }
    public int InvestedPoints { get; set; }

    public (StatType StatType, double multiplier)[] GetMultipliers()
    {
        return StatTypesToIncrease
            .Select(baseStatToIncrease =>
                (baseStatToIncrease.StatType, baseStatToIncrease.BaseMultiplier * InvestedPoints + 1))
            .ToArray();
    }
}