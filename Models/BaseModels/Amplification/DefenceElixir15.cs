using Models.Stats;

namespace Models.BaseModels.Amplification;

public class DefenceElixir15 : Elixir
{
    public DefenceElixir15()
        : base(
            "Эликсир обороны (15%)",
            new []
            {
                (StatType.Block, 1.15),
                (StatType.MagicalResistance, 1.15),
                (StatType.CriticalDamageResistance, 1.15),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}