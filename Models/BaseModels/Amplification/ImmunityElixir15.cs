using Models.Stats;

namespace Models.BaseModels.Amplification;

public class ImmunityElixir15 : Elixir
{
    public ImmunityElixir15()
        : base(
            "Эликсир иммунитета (15%)",
            new []
            {
                (StatType.CriticalDamageResistance, 1.15),
                (StatType.Armor, 1.15),
                (StatType.MagicArmor, 1.15),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}
