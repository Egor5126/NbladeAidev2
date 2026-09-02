using Models.BaseModels;

namespace Models.Names;

public static class CommonNamesProvider
{
    public static string GetStuffRarityName(StuffRarity stuffRarity)
    {
        return stuffRarity switch
        {
            StuffRarity.Violet => "Уникальная (фиолетовая)",
            StuffRarity.Orange => "Легендарная (оранжевая)",
            StuffRarity.Red => "Божественная (красная)",
            _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
        };
    }
}