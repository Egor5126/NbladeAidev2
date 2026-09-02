using Models.BaseModels;

namespace Models.Multipliers;

public static class StuffRuneOfImprovementMultiplier
{
    public static double RuneMultiplier(StuffRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            StuffRuneOfImprovementLevel.None => 1,
            StuffRuneOfImprovementLevel.Level8 => 1.84,
            StuffRuneOfImprovementLevel.Level12 => 2.0,
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }
}