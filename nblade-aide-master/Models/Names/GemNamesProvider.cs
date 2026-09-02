using Models.BaseModels;
using Models.Extensions;
using Models.Multipliers;

namespace Models.Names;

public static class GemNamesProvider
{
    public static string GetGemLevelName(GemLevel gemLevel)
    {
        return gemLevel switch
        {
            GemLevel.None => "нет",
            GemLevel.Level1 => "уровень 1",
            GemLevel.Level2 => "уровень 2",
            GemLevel.Level3 => "уровень 3",
            GemLevel.Level4 => "уровень 4",
            GemLevel.Level5 => "уровень 5",
            _ => throw new ArgumentOutOfRangeException(nameof(gemLevel), gemLevel, null)
        };
    }

    public static string GetWeaponGemLevelName(GemLevel gemLevel)
    {
        var gemLevelName = GetGemLevelName(gemLevel);

        if (gemLevel == GemLevel.None)
            return gemLevelName;
        return $"{gemLevelName}: {(StatNumberProvider.GetWeaponGemMultiplier(gemLevel)).ToPercentString()}";
    }
}