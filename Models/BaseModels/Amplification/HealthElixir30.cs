using Models.Stats;

namespace Models.BaseModels.Amplification;

public class HealthElixir10 : Elixir
{
    public HealthElixir10()
        : base(
            "Эликсир здоровья (10%)",
            new []
            {
                (StatType.Health, 1.1),
                (StatType.CriticalDamageResistance, 1.1),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}