using Models.Stats;

namespace Models.BaseModels.Amplification;

public class ImmunityElixir10 : Elixir
{
    public ImmunityElixir10()
        : base(
            "Эликсир иммунитета (10%)",
            new []
            {
                (StatType.CriticalDamageResistance, 1.1),
                (StatType.Armor, 1.1),
                (StatType.MagicArmor, 1.1),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}
