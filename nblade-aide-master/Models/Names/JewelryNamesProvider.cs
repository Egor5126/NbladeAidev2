using Models.BaseModels;
using Models.BaseModels.Jewelry;
using Models.StatCombinations;
using Models.Stats;

namespace Models.Names;

public static class JewelryNamesProvider
{
    public static string GetJewelryName(
        JewelryType jewelryType,
        StuffRarity stuffRarity,
        StatCombinationType statCombinationType)
    {
        return jewelryType switch
        {
            JewelryType.Amulet => statCombinationType switch
            {
                StatCombinationType.ThreeStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Улучшенная массивная реликвия",
                    StuffRarity.Orange => "Легендарный символ летучей мыши",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                StatCombinationType.FourStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Запредельная реликвия",
                    StuffRarity.Orange => "Легендарный символ ястреба",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            JewelryType.Necklace => statCombinationType switch
            {
                StatCombinationType.ThreeStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Улучшенный ледяной кристалл",
                    StuffRarity.Orange => "Легендарный амулет тройственности",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                StatCombinationType.FourStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Леденящий оберег",
                    StuffRarity.Orange => "Легендарное ожерелье дуализма",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            JewelryType.Ring => statCombinationType switch
            {
                StatCombinationType.ThreeStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Улучшенный змеиный перстень",
                    StuffRarity.Orange => "Легендарное кольцо повелителя амфибий",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                StatCombinationType.FourStats => stuffRarity switch
                {
                    StuffRarity.Violet => "Орихалковый перстень",
                    StuffRarity.Orange => "Легендарное кольцо зверобоя",
                    _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(jewelryType), jewelryType, null)
        };
    }

    public static string GetJewelrySimpleName(JewelryType jewelryType)
    {
        return jewelryType switch
        {
            JewelryType.Amulet => "амулет",
            JewelryType.Necklace => "ожерелье",
            JewelryType.Ring => "кольцо",
            _ => throw new ArgumentOutOfRangeException(nameof(jewelryType), jewelryType, null)
        };
    }
}