using Models.Stats;

namespace Models.BaseModels.Amplification;

public class BattleElixir15 : Elixir
{
    public BattleElixir15()
        : base(
            "Эликсир битвы (15%)",
            new []
            {
                (StatType.AttackPower, 1.15),
                (StatType.Accuracy, 1.15),
                (StatType.CriticalDamage, 1.15),
                (StatType.AttackSpeed, 1.15),
            })
    {
    }

    public override ElixirType ElixirType { get; } = ElixirType.Attack;
}