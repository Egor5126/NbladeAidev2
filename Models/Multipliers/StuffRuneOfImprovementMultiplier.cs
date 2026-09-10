using Models.BaseModels;

namespace Models.Multipliers;

public static class StuffRuneOfImprovementMultiplier
{
    public static double RuneMultiplier(StuffRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            StuffRuneOfImprovementLevel.None => 1,
            StuffRuneOfImprovementLevel.Level10 => 2.0,
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }
}