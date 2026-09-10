using Models;
using Models.BaseModels;
using Models.BaseModels.Amplification;
using Models.BaseModels.Armors;
using Models.BaseModels.Cloak;
using Models.BaseModels.Jewelry;
using Models.BaseModels.Weapon;
using Models.Stats;

namespace Models.Sharing;

// Кодирует и раскодирует билд персонажа в компактную строку, чтобы её можно было
// положить в ссылку и поделиться ею. Ничего не хранится на сервере — весь билд
// целиком лежит в самой ссылке.
public static class BuildCode
{
    private const char Separator = '.';

    public static string Encode(Character character)
    {
        var parts = new List<int> { (int)character.CharacterClass };

        EncodeCloak(character.Cloak, parts);

        EncodeJewelry(character.Amulet1, parts);
        EncodeJewelry(character.Amulet2, parts);
        EncodeJewelry(character.Necklace, parts);
        EncodeJewelry(character.Ring1, parts);
        EncodeJewelry(character.Ring2, parts);

        EncodeArmor(character.Helmet, parts);
        EncodeArmor(character.ShoulderPad, parts);
        EncodeArmor(character.Cuirass, parts);
        EncodeArmor(character.Highway, parts);
        EncodeArmor(character.Gloves, parts);
        EncodeArmor(character.Bracers, parts);
        EncodeArmor(character.Shoes, parts);
        EncodeArmor(character.Belt, parts);

        EncodeWeapon(character.MainWeapon, parts);

        if (character.SecondWeapon == null)
        {
            parts.Add(0);
        }
        else
        {
            parts.Add(1);
            EncodeWeapon(character.SecondWeapon, parts);
        }

        parts.Add(GetElixirIndex(character.AttackElixir, ElixirProvider.AttackElixirs));
        parts.Add(GetElixirIndex(character.DefenseElixir, ElixirProvider.DefenceElixirs));

        parts.Add(character.AvailableAmplifications.Length);
        foreach (var amplification in character.AvailableAmplifications)
        {
            parts.Add(amplification.Used ? 1 : 0);
        }

        return string.Join(Separator, parts);
    }

    public static bool TryDecode(string? code, out Character? character)
    {
        character = null;
        if (string.IsNullOrWhiteSpace(code))
            return false;

        var tokens = code.Split(Separator);
        var values = new int[tokens.Length];
        for (var i = 0; i < tokens.Length; i++)
        {
            if (!int.TryParse(tokens[i], out values[i]))
                return false;
        }

        try
        {
            var cursor = new Cursor(values);
            var characterClass = (CharacterClass)cursor.Next();
            var result = new Character(characterClass);

            result.Cloak = DecodeCloak(cursor, result.Cloak);

            result.Amulet1 = (Amulet)DecodeJewelry(cursor, result.Amulet1);
            result.Amulet2 = (Amulet)DecodeJewelry(cursor, result.Amulet2);
            result.Necklace = (Necklace)DecodeJewelry(cursor, result.Necklace);
            result.Ring1 = (Ring)DecodeJewelry(cursor, result.Ring1);
            result.Ring2 = (Ring)DecodeJewelry(cursor, result.Ring2);

            result.Helmet = (Helmet)DecodeArmor(cursor, result.Helmet);
            result.ShoulderPad = (ShoulderPad)DecodeArmor(cursor, result.ShoulderPad);
            result.Cuirass = (Cuirass)DecodeArmor(cursor, result.Cuirass);
            result.Highway = (Highway)DecodeArmor(cursor, result.Highway);
            result.Gloves = (Gloves)DecodeArmor(cursor, result.Gloves);
            result.Bracers = (Bracers)DecodeArmor(cursor, result.Bracers);
            result.Shoes = (Shoes)DecodeArmor(cursor, result.Shoes);
            result.Belt = (Belt)DecodeArmor(cursor, result.Belt);

            result.MainWeapon = DecodeWeapon(cursor, result.MainWeapon);

            var hasSecondWeapon = cursor.Next() == 1;
            result.SecondWeapon = hasSecondWeapon
                ? DecodeWeapon(cursor, result.SecondWeapon)
                : null;

            var attackElixirIndex = cursor.Next();
            result.AttackElixir = attackElixirIndex >= 0 && attackElixirIndex < ElixirProvider.AttackElixirs.Length
                ? ElixirProvider.AttackElixirs[attackElixirIndex]
                : null;

            var defenseElixirIndex = cursor.Next();
            result.DefenseElixir = defenseElixirIndex >= 0 && defenseElixirIndex < ElixirProvider.DefenceElixirs.Length
                ? ElixirProvider.DefenceElixirs[defenseElixirIndex]
                : null;

            var amplificationsCount = cursor.Next();
            for (var i = 0; i < amplificationsCount; i++)
            {
                var used = cursor.Next() == 1;
                if (i < result.AvailableAmplifications.Length)
                    result.AvailableAmplifications[i].Used = used;
            }

            character = result;
            return true;
        }
        catch
        {
            // Ссылка повреждена, устарела или сделана вручную с ошибкой — просто не открываем билд
            character = null;
            return false;
        }
    }

    private static void EncodeCloak(Cloak cloak, List<int> parts)
    {
        parts.Add((int)cloak.StatCombination.StatCombinationType);
        parts.Add(GetCombinationIndex(cloak.StatCombination));
        parts.Add((int)cloak.StuffRarity);
        parts.Add((int)cloak.RuneOfImprovementLevel);
    }

    private static Cloak DecodeCloak(Cursor cursor, Cloak current)
    {
        var combination = ReadCombination(cursor);
        var stuffRarity = (StuffRarity)cursor.Next();
        var runeLevel = (StuffRuneOfImprovementLevel)cursor.Next();
        return current.CreateFromInstance(combination, runeLevel, stuffRarity);
    }

    private static void EncodeJewelry(JewelryBase jewelry, List<int> parts)
    {
        parts.Add((int)jewelry.StatCombination.StatCombinationType);
        parts.Add(GetCombinationIndex(jewelry.StatCombination));
        parts.Add((int)jewelry.StuffRarity);
        parts.Add((int)jewelry.JewelryRuneOfImprovementLevel);
    }

    private static JewelryBase DecodeJewelry(Cursor cursor, JewelryBase current)
    {
        var combination = ReadCombination(cursor);
        var stuffRarity = (StuffRarity)cursor.Next();
        var runeLevel = (JewelryRuneOfImprovementLevel)cursor.Next();
        return current.CreateFromInstance(combination, runeLevel, stuffRarity);
    }

    private static void EncodeArmor(ArmorBase armor, List<int> parts)
    {
        parts.Add((int)armor.StatCombination.StatCombinationType);
        parts.Add(GetCombinationIndex(armor.StatCombination));
        parts.Add((int)armor.RuneOfImprovementLevel);
        parts.Add(armor.Gem1.StatType.HasValue ? (int)armor.Gem1.StatType.Value : -1);
        parts.Add((int)armor.Gem1.GemLevel);
        parts.Add(armor.Gem2.StatType.HasValue ? (int)armor.Gem2.StatType.Value : -1);
        parts.Add((int)armor.Gem2.GemLevel);
    }

    private static ArmorBase DecodeArmor(Cursor cursor, ArmorBase current)
    {
        var combination = ReadCombination(cursor);
        var runeLevel = (StuffRuneOfImprovementLevel)cursor.Next();
        var gem1StatTypeRaw = cursor.Next();
        var gem1Level = (PowerStoneLevel)cursor.Next();
        var gem2StatTypeRaw = cursor.Next();
        var gem2Level = (ConcentrationStoneLevel)cursor.Next();

        StatType? gem1StatType = gem1StatTypeRaw == -1 ? null : (StatType)gem1StatTypeRaw;
        StatType? gem2StatType = gem2StatTypeRaw == -1 ? null : (StatType)gem2StatTypeRaw;

        var result = current.CreateFromInstance(combination, runeLevel);
        result.Gem1 = new Gem(gem1StatType, gem1Level);
        result.Gem2 = new ConcentrationGem(gem2StatType, gem2Level);
        return result;
    }

    private static void EncodeWeapon(WeaponBase weapon, List<int> parts)
    {
        parts.Add((int)weapon.WeaponType);
        parts.Add((int)weapon.StatCombination.StatCombinationType);
        parts.Add(GetCombinationIndex(weapon.StatCombination));
        parts.Add((int)weapon.RuneOfImprovementLevel);
        parts.Add(weapon.WeaponGems.Count);
        foreach (var gem in weapon.WeaponGems)
        {
            parts.Add((int)gem.StatType);
            parts.Add((int)gem.GemLevel);
        }
    }

    private static WeaponBase DecodeWeapon(Cursor cursor, WeaponBase current)
    {
        var weaponType = (WeaponType)cursor.Next();
        var combination = ReadCombination(cursor);
        var runeLevel = (StuffRuneOfImprovementLevel)cursor.Next();
        var gemCount = cursor.Next();

        var gems = new (StatType StatType, GemLevel GemLevel)[gemCount];
        for (var i = 0; i < gemCount; i++)
        {
            gems[i] = ((StatType)cursor.Next(), (GemLevel)cursor.Next());
        }

        var weapon = current.WeaponType == weaponType ? current : current.CreateFromWeaponType(weaponType);
        weapon = weapon.CreateFromInstance(combination, runeLevel);

        for (var i = 0; i < gems.Length && i < weapon.WeaponGems.Count; i++)
        {
            weapon.WeaponGems[i] = new WeaponGem(gems[i].StatType, gems[i].GemLevel);
        }

        return weapon;
    }

    private static StatCombination ReadCombination(Cursor cursor)
    {
        var type = (StatCombinationType)cursor.Next();
        var index = cursor.Next();
        var pool = type == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();

        if (pool.Length == 0)
            throw new InvalidOperationException("No stat combinations available.");

        index = Math.Clamp(index, 0, pool.Length - 1);
        return pool[index];
    }

    private static int GetCombinationIndex(StatCombination combination)
    {
        var pool = combination.StatCombinationType == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();

        for (var i = 0; i < pool.Length; i++)
        {
            if (StatTypesMatch(pool[i], combination))
                return i;
        }

        // На случай если комбинацию не удалось найти (не должно происходить) —
        // берём первую из списка, чтобы билд всё равно сохранился, пусть и неточно.
        return 0;
    }

    private static bool StatTypesMatch(StatCombination a, StatCombination b)
    {
        if (a.OriginStats.Length != b.OriginStats.Length)
            return false;

        for (var i = 0; i < a.OriginStats.Length; i++)
        {
            if (a.OriginStats[i].StatType != b.OriginStats[i].StatType)
                return false;
        }

        return true;
    }

    private static int GetElixirIndex(Elixir? elixir, Elixir[] pool)
    {
        if (elixir == null)
            return -1;

        for (var i = 0; i < pool.Length; i++)
        {
            if (pool[i].GetType() == elixir.GetType())
                return i;
        }

        return -1;
    }

    private sealed class Cursor
    {
        private readonly int[] _values;
        private int _index;

        public Cursor(int[] values)
        {
            _values = values;
        }

        public int Next()
        {
            if (_index >= _values.Length)
                throw new IndexOutOfRangeException("Build code is too short.");
            return _values[_index++];
        }
    }
}
