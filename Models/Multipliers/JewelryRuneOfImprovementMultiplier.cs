using Models.BaseModels;

namespace Models.Multipliers;

public static class JewelryRuneOfImprovementMultiplier
{
    public static double RuneMultiplier(JewelryRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            JewelryRuneOfImprovementLevel.None => 1,
            JewelryRuneOfImprovementLevel.Level10 => 2.0,
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }
}