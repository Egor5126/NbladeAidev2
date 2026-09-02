using Models.Stats;

namespace Models.BaseModels.Amplification;

public class AgilityElixir30 : Elixir
{
    public AgilityElixir30()
        : base(
            "Эликсир ловкости (15%)",
            new []
            {
                (StatType.Parry, 1.15),
                (StatType.Evasion, 1.15),
                (StatType.CriticalDamageResistance, 1.15),
                (StatType.MagicalResistance, 1.15),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}