using Models.Stats;

namespace Models.BaseModels.Amplification;

public class DefenceElixir10 : Elixir
{
    public DefenceElixir10()
        : base(
            "Эликсир обороны (10%)",
            new []
            {
                (StatType.Block, 1.1),
                (StatType.MagicalResistance, 1.1),
                (StatType.CriticalDamageResistance, 1.1),
                (StatType.Armor, 1.1),
                (StatType.MagicArmor, 1.1),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Defense;
}