using Models.BaseModels;
using Models.BaseModels.Armors;
using Models.BaseModels.Weapon;

namespace Models.Multipliers;

public static class BaseStuffStatsMultiplier
{
    public static double GetArmorGreenStatMultiplier(ArmorType armorType)
    {
        return armorType switch
        {
            ArmorType.Helmet => 2.5,
            ArmorType.ShoulderPad => 2.5,
            ArmorType.Cuirass => 3.5,
            ArmorType.Highway => 2.5,
            ArmorType.Gloves => 1,
            ArmorType.Shoes => 2,
            ArmorType.Bracers => 1,
            ArmorType.Belt => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
        };
    }

    public static double GetWeaponGreenStatMultiplier(WeaponHandedClass weaponHandedClass)
    {
        return weaponHandedClass switch
        {
            WeaponHandedClass.OneHandedWeapon => 2,
            WeaponHandedClass.TwoHandedWeapon => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(weaponHandedClass), weaponHandedClass, null)
        };
    }

    public static double GetCloakGreenStatMultiplier()
    {
        return 3;
    }

    public static double GetJewelryGreenStatMultiplier()
    {
        return 1;
    }

    public static double GetStuffRarityMultiplier(StuffRarity stuffRarity)
    {
        return stuffRarity switch
        {
            StuffRarity.Violet => 1,
            StuffRarity.Orange => 1.25,
            StuffRarity.Red => 1.5875,
            _ => throw new ArgumentOutOfRangeException(nameof(stuffRarity), stuffRarity, null)
        };
    }

    public static double GetArmorWhiteStatMultiplier(ArmorType armorType)
    {
        return armorType switch
        {
            ArmorType.Helmet => 2,
            ArmorType.ShoulderPad => 3,
            ArmorType.Cuirass => 4,
            ArmorType.Highway => 4,
            ArmorType.Gloves => 2,
            ArmorType.Shoes => 2,
            ArmorType.Bracers => 1,
            ArmorType.Belt => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
        };
    }
}