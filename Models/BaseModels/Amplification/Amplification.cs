using Models.Stats;

namespace Models.BaseModels.Amplification;

public abstract class Amplification
{
    protected Amplification(string name, (StatType StatType, double BaseMultiplier)[] statTypesToIncrease, string description)
    {
        Used = false;
        Name = name;
        StatTypesToIncrease = statTypesToIncrease;
        Description = description;
    }

    public bool Used { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public (StatType StatType, double BaseMultiplier)[] StatTypesToIncrease { get; set; }
}