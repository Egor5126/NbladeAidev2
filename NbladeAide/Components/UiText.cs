using System.Globalization;
using Models.BaseModels;
using Models.Names;
using Models.Stats;

namespace NbladeAide.Components;

// Небольшие помощники для оформления текста в интерфейсе: цвета редкости,
// форматирование чисел и подписи характеристик.
public static class UiText
{
    public const string Violet = "#B58CFF";
    public const string Orange = "#FF9B3D";
    public const string Red = "#FF6B6B";

    public static string RarityColor(StuffRarity rarity)
    {
        return rarity switch
        {
            StuffRarity.Violet => Violet,
            StuffRarity.Orange => Orange,
            StuffRarity.Red => Red,
            _ => Violet
        };
    }

    public static string Capitalize(string text)
    {
        return string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0]) + text.Substring(1);
    }

    public static string StatName(StatType statType)
    {
        return Capitalize(StatNamesProvider.GetStatName(statType));
    }

    // 88340 -> "88 340" (неразрывный пробел), чтобы большие числа читались легче.
    public static string FormatNumber(int value)
    {
        return value.ToString("N0", CultureInfo.InvariantCulture).Replace(",", " ");
    }

    // Строка характеристики в том виде, как она показывается в подсказке игры:
    // "Сила атаки: +1326-1989 (1629)" — если у стата есть диапазон, иначе просто "+значение".
    public static string StatTooltipText(StatCombination combination, int index)
    {
        var stat = combination.Stats[index];
        var name = StatName(stat.StatType);

        if (combination is CustomStatCombination custom && custom.GetMax(index) > custom.GetMin(index))
            return $"{name}: +{custom.GetMin(index)}-{custom.GetMax(index)} ({stat.StatNumber})";

        return $"{name}: +{stat.StatNumber}";
    }
}
