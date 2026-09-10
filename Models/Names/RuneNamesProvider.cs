using Models.BaseModels;

namespace Models.Names;

public static class RuneNamesProvider
{
    public static string GetStuffRuneName(StuffRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            StuffRuneOfImprovementLevel.None => "нет",
            StuffRuneOfImprovementLevel.Level10 => "+10",
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }

    public static string GetJewelryRuneName(JewelryRuneOfImprovementLevel runeLevel)
    {
        return runeLevel switch
        {
            JewelryRuneOfImprovementLevel.None => "нет",
            JewelryRuneOfImprovementLevel.Level10 => "+10",
            _ => throw new ArgumentOutOfRangeException(nameof(runeLevel), runeLevel, null)
        };
    }
}