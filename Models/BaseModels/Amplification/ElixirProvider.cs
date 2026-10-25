namespace Models.BaseModels.Amplification;

public static class ElixirProvider
{
    public static Elixir[] AttackElixirs = {
        new BattleElixir10(),
        new BattleElixir15(),
    };

    public static Elixir[] DefenceElixirs = {
        new AgilityElixir15(),
        new AgilityElixir30(),
        new HealthElixir10(),
        new HealthElixir15(),
        new DefenceElixir10(),
        new DefenceElixir15(),
        new ImmunityElixir10(),
        new ImmunityElixir15(),
    };
}