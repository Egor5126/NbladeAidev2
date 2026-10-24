using Models.Stats;

namespace Models.BaseModels.Amplification;

public class BattleElixir10 : Elixir
{
    public BattleElixir10()
        : base(
            "Эликсир битвы (10%)",
            new []
            {
                (StatType.AttackPower, 1.1),
                (StatType.Accuracy, 1.1),
                (StatType.CriticalChance, 1.1),
                (StatType.AttackSpeed, 1.1),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Attack;
}