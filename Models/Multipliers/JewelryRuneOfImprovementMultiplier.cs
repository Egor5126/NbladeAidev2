using Models.BaseModels;

namespace Models.Multipliers;

public static class JewelryRuneOfImprovementMultiplier
{
    public static double RuneMultiplier(JewelryRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            JewelryRuneOfImprovementLevel.None => 1,
            JewelryRuneOfImprovementLevel.Level8 => 2.15,
            JewelryRuneOfImprovementLevel.Level12 => 2.35,
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }
}