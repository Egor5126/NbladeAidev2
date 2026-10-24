using Models.BaseModels.Weapon;
using Models.StatCombinations;
using Models.Stats;

namespace Models.Names;

public static class WeaponNamesProvider
{
    public static string GetWeaponName(WeaponType weaponType, StatCombinationType statCombinationType)
    {
        return weaponType switch
        {
            WeaponType.Shield => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный мифрильный щит",
                StatCombinationType.FourStats => "Непробиваемый щит",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Blade => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный адамантиновый клинок",
                StatCombinationType.FourStats => "Божественный клинок",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Flamberge => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный адамантиновый фламберг",
                StatCombinationType.FourStats => "Улучшенный волшебный фламберг",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Halberd => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенная адамантиновая алебарда",
                StatCombinationType.FourStats => "Божественная алебарда",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Staff => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный боевой посох",
                StatCombinationType.FourStats => "Улучшенный боевой посох",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Bow => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный небесный лук",
                StatCombinationType.FourStats => "Божественный лук",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Crossbow => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный охотничий арбалет",
                StatCombinationType.FourStats => "Небесный арбалет",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Dagger => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный адамантиновый кинжал",
                StatCombinationType.FourStats => "Мастерский кинжал",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Axe => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенная адамантиновая секира",
                StatCombinationType.FourStats => "Божественная секира",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Mint => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный адамантиновый чекан",
                StatCombinationType.FourStats => "Патриарший молот",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            WeaponType.Hammer => statCombinationType switch
            {
                StatCombinationType.ThreeStats => "Улучшенный адамантиновый молот",
                StatCombinationType.FourStats => "Патриарший двуручный молот",
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
        };
    }

    public static string GetWeaponNameByType(WeaponType weaponType)
    {
        return weaponType switch
        {
            WeaponType.Shield => "Щит",
            WeaponType.Blade => "Одноручный меч",
            WeaponType.Flamberge => "Двуручный меч",
            WeaponType.Halberd => "Двуручный топор",
            WeaponType.Staff => "Посох",
            WeaponType.Bow => "Лук",
            WeaponType.Crossbow => "Арбалет",
            WeaponType.Dagger => "Кинжал",
            WeaponType.Axe => "Топор",
            WeaponType.Mint => "Одноручный молот",
            WeaponType.Hammer => "Двуручный молот",
            _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
        };
    }

    public static string GetWeaponSimpleName(WeaponOrder weaponOrder)
    {
        return weaponOrder switch
        {
            WeaponOrder.Main => "основное оружие",
            WeaponOrder.Second => "дополнительное оружие",
            _ => throw new ArgumentOutOfRangeException(nameof(weaponOrder), weaponOrder, null)
        };
    }
}