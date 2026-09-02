using Models.BaseModels;
using Models.BaseModels.Armors;
using Models.StatCombinations;
using Models.Stats;

namespace Models.Names;

public static class ArmorNamesProvider
{
    public static string GetArmorName(ArmorType armorType, StatCombinationType statCombinationType, CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => statCombinationType switch
            {
                StatCombinationType.ThreeStats => armorType switch
                {
                    ArmorType.Helmet => "Улучшенный мифрильный шлем",
                    ArmorType.ShoulderPad => "Улучшенные мифрильные наплечники",
                    ArmorType.Cuirass => "Улучшенная мифрильная кираса",
                    ArmorType.Highway => "Улучшенные мифрильные поножи",
                    ArmorType.Gloves => "Улучшенные мифрильные рукавицы",
                    ArmorType.Shoes => "Улучшенные мифрильные ботинки",
                    ArmorType.Bracers => "Улучшенные мифрильные наручи",
                    ArmorType.Belt => "Улучшенный мифрильный пояс",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                StatCombinationType.FourStats => armorType switch
                {
                    ArmorType.Helmet => "Непробиваемый топхельм",
                    ArmorType.ShoulderPad => "Непробиваемые наплечники",
                    ArmorType.Cuirass => "Непробиваемая кираса",
                    ArmorType.Highway => "Непробиваемые поножи",
                    ArmorType.Gloves => "Непробиваемые рукавицы",
                    ArmorType.Shoes => "Непробиваемые ботинки",
                    ArmorType.Bracers => "Непробиваемые наручи",
                    ArmorType.Belt => "Непробиваемый пояс",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            CharacterClass.Bandit => statCombinationType switch
            {
                StatCombinationType.ThreeStats => armorType switch
                {
                    ArmorType.Helmet => "Улучшенная шляпа авантюриста",
                    ArmorType.ShoulderPad => "Улучшенные наплечники авантюриста",
                    ArmorType.Cuirass => "Улучшенная куртка авантюриста",
                    ArmorType.Highway => "Улучшенные штаны авантюриста",
                    ArmorType.Gloves => "Улучшенные перчатки авантюриста",
                    ArmorType.Shoes => "Улучшенные сапоги авантюриста",
                    ArmorType.Bracers => "Улучшенные наручи авантюриста",
                    ArmorType.Belt => "Улучшенный пояс авантюриста",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                StatCombinationType.FourStats => armorType switch
                {
                    ArmorType.Helmet => "Мастерская шляпа",
                    ArmorType.ShoulderPad => "Мастерский наплечник",
                    ArmorType.Cuirass => "Мастерская куртка",
                    ArmorType.Highway => "Мастерские штаны",
                    ArmorType.Gloves => "Мастерские перчатки",
                    ArmorType.Shoes => "Мастерские ботинки",
                    ArmorType.Bracers => "Мастерские наручи",
                    ArmorType.Belt => "Мастерский пояс",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            CharacterClass.Priest => statCombinationType switch
            {
                StatCombinationType.ThreeStats => armorType switch
                {
                    ArmorType.Helmet => "Улучшенный клобук пророка",
                    ArmorType.ShoulderPad => "Улучшенные наплечники пророка",
                    ArmorType.Cuirass => "Улучшенная роба пророка",
                    ArmorType.Highway => "Улучшенные штаны пророка",
                    ArmorType.Gloves => "Улучшенные перчатки пророка",
                    ArmorType.Shoes => "Улучшенные ботинки пророка",
                    ArmorType.Bracers => "Улучшенные наручи пророка",
                    ArmorType.Belt => "Улучшенный пояс пророка",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                StatCombinationType.FourStats => armorType switch
                {
                    ArmorType.Helmet => "Патриарший венец",
                    ArmorType.ShoulderPad => "Патриаршие наплечники",
                    ArmorType.Cuirass => "Патриарший нагрудник",
                    ArmorType.Highway => "Патриаршие поножи",
                    ArmorType.Gloves => "Патриаршие перчатки",
                    ArmorType.Shoes => "Патриаршие сапоги",
                    ArmorType.Bracers => "Патриаршие наручи",
                    ArmorType.Belt => "Патриарший пояс",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            CharacterClass.Archer => statCombinationType switch
            {
                StatCombinationType.ThreeStats => armorType switch
                {
                    ArmorType.Helmet => "Улучшенная охотничья шляпа",
                    ArmorType.ShoulderPad => "Улучшенные охотничьи эполеты",
                    ArmorType.Cuirass => "Улучшенный охотничий мундир",
                    ArmorType.Highway => "Улучшенные охотничьи брюки",
                    ArmorType.Gloves => "Улучшенные охотничьи перчатки",
                    ArmorType.Shoes => "Улучшенные охотничьи сапоги",
                    ArmorType.Bracers => "Улучшенные охотничьи манжеты",
                    ArmorType.Belt => "Улучшенный охотничий ремень",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                StatCombinationType.FourStats => armorType switch
                {
                    ArmorType.Helmet => "Небесный шлем",
                    ArmorType.ShoulderPad => "Небесные наплечники",
                    ArmorType.Cuirass => "Небесный мундир",
                    ArmorType.Highway => "Небесные брюки",
                    ArmorType.Gloves => "Небесные перчатки",
                    ArmorType.Shoes => "Небесные сапоги",
                    ArmorType.Bracers => "Небесные манжеты",
                    ArmorType.Belt => "Небесный ремень",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            CharacterClass.Wizard => statCombinationType switch
            {
                StatCombinationType.ThreeStats => armorType switch
                {
                    ArmorType.Helmet => "Улучшенная корона архимага",
                    ArmorType.ShoulderPad => "Улучшенные наплечники архимага",
                    ArmorType.Cuirass => "Улучшенная роба архимага",
                    ArmorType.Highway => "Улучшенные штаны архимага",
                    ArmorType.Gloves => "Улучшенные перчатки архимага",
                    ArmorType.Shoes => "Улучшенные ботинки архимага",
                    ArmorType.Bracers => "Улучшенные браслеты архимага",
                    ArmorType.Belt => "Улучшенный пояс архимага",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                StatCombinationType.FourStats => armorType switch
                {
                    ArmorType.Helmet => "Нетленная корона",
                    ArmorType.ShoulderPad => "Нетленные наплечники",
                    ArmorType.Cuirass => "Нетленная роба",
                    ArmorType.Highway => "Нетленные штаны",
                    ArmorType.Gloves => "Нетленные перчатки",
                    ArmorType.Shoes => "Нетленные сапоги",
                    ArmorType.Bracers => "Нетленные браслеты",
                    ArmorType.Belt => "Нетленный пояс",
                    _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(statCombinationType), statCombinationType, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }

    public static string GetArmorSimpleName(ArmorType armorType)
    {
        return armorType switch
        {
            ArmorType.Helmet => "головной убор",
            ArmorType.ShoulderPad => "наплечники",
            ArmorType.Cuirass => "доспех",
            ArmorType.Highway => "штаны",
            ArmorType.Gloves => "перчатки",
            ArmorType.Shoes => "обувь",
            ArmorType.Bracers => "наручи",
            ArmorType.Belt => "пояс",
            _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
        };
    }
}