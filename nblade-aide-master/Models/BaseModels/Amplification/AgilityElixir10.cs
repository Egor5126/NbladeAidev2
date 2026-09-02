using Models.Stats;

namespace Models.BaseModels.Amplification;

public class AgilityElixir15 : Elixir
{
    public AgilityElixir15()
        : base(
            "Эликсир ловкости (10%)",
            new []
            {
                (StatType.Parry, 1.1),
                (StatType.Evasion, 1.1),
                (StatType.CriticalDamageResistance, 1.1),
                (StatType.MagicalResistance, 1.1),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}