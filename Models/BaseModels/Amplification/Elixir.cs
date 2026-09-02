using Models.Stats;

namespace Models.BaseModels.Amplification;

public abstract class Elixir
{
    protected Elixir(string name, (StatType StatType, double BaseMultiplier)[] statTypesToIncrease)
    {
        Name = name;
        StatTypesToIncrease = statTypesToIncrease;
    }

    public string Name { get; set; }
    public (StatType StatType, double BaseMultiplier)[] StatTypesToIncrease { get; set; }

    public abstract ElixirType ElixirType { get; }
}