using Models.Stats;

namespace Models.BaseModels.Amplification;

public class HealthElixir15 : Elixir
{
    public HealthElixir15()
        : base(
            "Эликсир здоровья (15%)",
            new []
            {
                (StatType.Health, 1.15),
                (StatType.CriticalDamageResistance, 1.15),
                (StatType.Armor, 1.15),
                (StatType.MagicArmor, 1.15),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}