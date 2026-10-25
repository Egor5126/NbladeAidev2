using System.Globalization;
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
//
// Формат: «v2.<число>.<число>…». Первый токен — версия формата. Строки без версии (первые ссылки)
// читаются как версия 1: там эликсиры и усиления хранились по порядку в списке, в версии 2 —
// по постоянным номерам из таблиц ниже. В версии 3 в конец добавлена аура воина (0 — нет);
// в версии 4 — аура лучника; ссылки версий 1–2 читаются без аур, версии 3 — без ауры лучника.
//
// Правила, чтобы чужие ссылки не ломались:
//  - числа enum'ов, которые попадают в ссылку, не менять и не переставлять (новые — только в конец);
//  - номера в ElixirIds и AmplificationIds не менять и не переиспользовать;
//  - если структуру данных после версии нужно изменить — увеличить CurrentVersion и
//    оставить чтение старой версии.
public static class BuildCode
{
    private const char Separator = '.';
    private const char VersionMarker = 'v';
    private const int CurrentVersion = 4;

    // Защита от испорченных/подделанных ссылок: не даём заказать огромные массивы.
    private const int MaxStatsPerCombination = 8;
    private const int MaxWeaponGems = 16;
    private const int MaxAmplifications = 32;

    // Постоянные номера эликсиров (0 зарезервирован под «нет эликсира»).
    // Привязаны к классам, а не к тому, как называется эликсир в интерфейсе.
    private static readonly (int Id, Type Type)[] ElixirIds =
    {
        (1, typeof(BattleElixir10)),
        (2, typeof(BattleElixir15)),
        (10, typeof(AgilityElixir15)),
        (11, typeof(AgilityElixir30)),
        (12, typeof(HealthElixir10)),
        (13, typeof(HealthElixir15)),
        (14, typeof(DefenceElixir10)),
        (15, typeof(DefenceElixir15)),
        (16, typeof(ImmunityElixir10)),
        (17, typeof(ImmunityElixir15)),
    };

    // Постоянные номера усилений.
    private static readonly (int Id, Type Type)[] AmplificationIds =
    {
        (1, typeof(TrainedFighters)),
        (2, typeof(UniversalSoldiers)),
    };

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

        parts.Add(GetId(character.AttackElixir, ElixirIds, "эликсир"));
        parts.Add(GetId(character.DefenseElixir, ElixirIds, "эликсир"));

        parts.Add(character.AvailableAmplifications.Length);
        foreach (var amplification in character.AvailableAmplifications)
        {
            parts.Add(GetId(amplification, AmplificationIds, "усиление"));
            parts.Add(amplification.Used ? 1 : 0);
        }

        parts.Add((int)character.WarriorAura);
        parts.Add((int)character.ArcherAura);

        return $"{VersionMarker}{CurrentVersion}{Separator}{string.Join(Separator, parts)}";
    }

    public static bool TryDecode(string? code, out Character? character)
    {
        return TryDecode(code, out character, out _);
    }

    // error — короткое объяснение, что не так со ссылкой (для логов и отладки).
    public static bool TryDecode(string? code, out Character? character, out string? error)
    {
        character = null;
        error = null;

        if (string.IsNullOrWhiteSpace(code))
        {
            error = "Пустой код билда.";
            return false;
        }

        var tokens = code.Trim().Split(Separator);

        var version = 1;
        var firstValueToken = 0;
        if (tokens[0].Length > 0 && char.ToLowerInvariant(tokens[0][0]) == VersionMarker)
        {
            if (!int.TryParse(tokens[0].AsSpan(1), NumberStyles.None, CultureInfo.InvariantCulture, out version))
            {
                error = $"Не удалось прочитать версию билда «{tokens[0]}».";
                return false;
            }

            firstValueToken = 1;
        }

        if (version < 1 || version > CurrentVersion)
        {
            error = $"Неизвестная версия билда: {version} (поддерживаются 1–{CurrentVersion}).";
            return false;
        }

        var values = new int[tokens.Length - firstValueToken];
        for (var i = 0; i < values.Length; i++)
        {
            if (!int.TryParse(tokens[i + firstValueToken], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out values[i]))
            {
                error = $"Не число в позиции {i + firstValueToken}: «{tokens[i + firstValueToken]}».";
                return false;
            }
        }

        try
        {
            var cursor = new Cursor(values);
            var characterClass = ReadEnum<CharacterClass>(cursor, "класс персонажа");
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

            result.AttackElixir = ReadElixir(cursor, version, ElixirProvider.AttackElixirs, "атакующий эликсир");
            result.DefenseElixir = ReadElixir(cursor, version, ElixirProvider.DefenceElixirs, "защитный эликсир");

            DecodeAmplifications(cursor, version, result);

            if (version >= 3)
                result.WarriorAura = ReadEnum<WarriorAura>(cursor, "аура воина");

            if (version >= 4)
                result.ArcherAura = ReadEnum<ArcherAura>(cursor, "аура лучника");

            character = result;
            return true;
        }
        catch (Exception ex) when (ex is BuildCodeFormatException
                                       or IndexOutOfRangeException
                                       or InvalidCastException
                                       or ArgumentException
                                       or InvalidOperationException)
        {
            // Ссылка повреждена, устарела или сделана вручную с ошибкой — просто не открываем билд,
            // а причину отдаём наружу, чтобы её было видно при отладке.
            character = null;
            error = ex.Message;
            return false;
        }
    }

    private static void EncodeCloak(Cloak cloak, List<int> parts)
    {
        EncodeCombination(cloak.StatCombination, parts);
        parts.Add((int)cloak.StuffRarity);
        parts.Add((int)cloak.RuneOfImprovementLevel);
        EncodeStatValues(cloak.StatCombination, parts);
    }

    private static Cloak DecodeCloak(Cursor cursor, Cloak current)
    {
        var combination = NormalizeCloakCombination(ReadCombination(cursor));
        var stuffRarity = ReadEnum<StuffRarity>(cursor, "редкость плаща");
        var runeLevel = ReadEnum<StuffRuneOfImprovementLevel>(cursor, "руна плаща");
        var result = current.CreateFromInstance(combination, runeLevel, stuffRarity);
        DecodeStatValues(cursor, result.StatCombination);
        return result;
    }

    // Плащ всегда с 4 характеристиками. Старые ссылки могли хранить его как трёхстатовый —
    // приводим к 4 характеристикам, значения и диапазоны выбранных статов сохраняются.
    private static StatCombination NormalizeCloakCombination(StatCombination combination)
    {
        if (combination.StatCombinationType != StatCombinationType.ThreeStats)
            return combination;

        if (combination is EmptyStatCombination)
            return EmptyStatCombination.FourStats;

        if (combination is CustomStatCombination custom)
        {
            var stats = new (StatType StatType, int Min, int Max, int Value)[custom.Stats.Length];
            for (var i = 0; i < stats.Length; i++)
                stats[i] = (custom.Stats[i].StatType, custom.GetMin(i), custom.GetMax(i), custom.Stats[i].StatNumber);
            return new CustomStatCombination(StatCombinationType.FourStats, stats);
        }

        return combination;
    }

    private static void EncodeJewelry(JewelryBase jewelry, List<int> parts)
    {
        EncodeCombination(jewelry.StatCombination, parts);
        parts.Add((int)jewelry.StuffRarity);
        parts.Add((int)jewelry.JewelryRuneOfImprovementLevel);
    }

    private static JewelryBase DecodeJewelry(Cursor cursor, JewelryBase current)
    {
        var combination = ReadCombination(cursor);
        var stuffRarity = ReadEnum<StuffRarity>(cursor, "редкость бижутерии");
        var runeLevel = ReadEnum<JewelryRuneOfImprovementLevel>(cursor, "руна бижутерии");
        return current.CreateFromInstance(combination, runeLevel, stuffRarity);
    }

    private static void EncodeArmor(ArmorBase armor, List<int> parts)
    {
        EncodeCombination(armor.StatCombination, parts);
        parts.Add((int)armor.RuneOfImprovementLevel);
        parts.Add(armor.Gem1.StatType.HasValue ? (int)armor.Gem1.StatType.Value : -1);
        parts.Add((int)armor.Gem1.GemLevel);
        parts.Add(armor.Gem2.StatType.HasValue ? (int)armor.Gem2.StatType.Value : -1);
        parts.Add((int)armor.Gem2.GemLevel);
    }

    private static ArmorBase DecodeArmor(Cursor cursor, ArmorBase current)
    {
        var combination = ReadCombination(cursor);
        var runeLevel = ReadEnum<StuffRuneOfImprovementLevel>(cursor, "руна брони");
        var gem1StatType = ReadOptionalStatType(cursor, "характеристика камня силы");
        var gem1Level = ReadEnum<PowerStoneLevel>(cursor, "уровень камня силы");
        var gem2StatType = ReadOptionalStatType(cursor, "характеристика камня концентрации");
        var gem2Level = ReadEnum<ConcentrationStoneLevel>(cursor, "уровень камня концентрации");

        var result = current.CreateFromInstance(combination, runeLevel);
        result.Gem1 = new Gem(gem1StatType, gem1Level);
        result.Gem2 = new ConcentrationGem(gem2StatType, gem2Level);
        return result;
    }

    private static void EncodeWeapon(WeaponBase weapon, List<int> parts)
    {
        parts.Add((int)weapon.WeaponType);
        EncodeCombination(weapon.StatCombination, parts);
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
        var weaponType = ReadEnum<WeaponType>(cursor, "тип оружия");
        var combination = ReadCombination(cursor);
        var runeLevel = ReadEnum<StuffRuneOfImprovementLevel>(cursor, "руна оружия");
        var gemCount = cursor.NextCount(MaxWeaponGems, "число камней оружия");

        var gems = new (StatType StatType, GemLevel GemLevel)[gemCount];
        for (var i = 0; i < gemCount; i++)
        {
            gems[i] = (ReadEnum<StatType>(cursor, "характеристика камня оружия"),
                ReadEnum<GemLevel>(cursor, "уровень камня оружия"));
        }

        var weapon = current.WeaponType == weaponType ? current : current.CreateFromWeaponType(weaponType);
        weapon = weapon.CreateFromInstance(combination, runeLevel);

        for (var i = 0; i < gems.Length && i < weapon.WeaponGems.Count; i++)
        {
            weapon.WeaponGems[i] = new WeaponGem(gems[i].StatType, gems[i].GemLevel);
        }

        return weapon;
    }

    // Хранит значения характеристик выбранной комбинации, которые игрок мог
    // вручную поправить в редакторе — чтобы они не терялись при шаринге билда.
    private static void EncodeStatValues(StatCombination combination, List<int> parts)
    {
        parts.Add(combination.Stats.Length);
        foreach (var stat in combination.Stats)
        {
            parts.Add(stat.StatNumber);
        }
    }

    private static void DecodeStatValues(Cursor cursor, StatCombination combination)
    {
        var count = cursor.NextCount(MaxStatsPerCombination, "число значений характеристик");
        for (var i = 0; i < count; i++)
        {
            var value = cursor.Next();
            if (i < combination.Stats.Length)
                combination.Stats[i].StatNumber = value;
        }
    }

    // Сохраняет комбинацию характеристик вещи: либо ссылку на именованную комбинацию
    // из общего списка (индекс >= 0), либо специальное состояние — "ещё не выбрана"
    // (-1) или "собрана игроком вручную" (-2, с полным списком стат+мин+макс+значение).
    //
    // Индекс именованной комбинации — это позиция в StatCombinationsList, поэтому список
    // можно только дополнять в конец. Если комбинацию в списке найти не удалось, она сохраняется
    // как ручная (-2) с теми же числами — так билд не искажается.
    private static void EncodeCombination(StatCombination combination, List<int> parts)
    {
        parts.Add((int)combination.StatCombinationType);

        if (combination is EmptyStatCombination)
        {
            parts.Add(-1);
            return;
        }

        if (combination is not CustomStatCombination && TryGetCombinationIndex(combination, out var index))
        {
            parts.Add(index);
            return;
        }

        var custom = combination as CustomStatCombination;
        parts.Add(-2);
        parts.Add(combination.Stats.Length);
        for (var i = 0; i < combination.Stats.Length; i++)
        {
            var value = combination.Stats[i].StatNumber;
            parts.Add((int)combination.Stats[i].StatType);
            parts.Add(custom?.GetMin(i) ?? value);
            parts.Add(custom?.GetMax(i) ?? value);
            parts.Add(value);
        }
    }

    private static StatCombination ReadCombination(Cursor cursor)
    {
        var type = ReadEnum<StatCombinationType>(cursor, "тип комбинации характеристик");
        var index = cursor.Next();

        // -1 значит, что игрок ещё не выбрал конкретную комбинацию характеристик для этой вещи.
        if (index == -1)
            return EmptyStatCombination.For(type);

        // -2 значит, что комбинация собрана игроком вручную — читаем её статы,
        // диапазоны и текущие значения целиком.
        if (index == -2)
        {
            var count = cursor.NextCount(MaxStatsPerCombination, "число характеристик вещи");
            var stats = new (StatType StatType, int Min, int Max, int Value)[count];
            for (var i = 0; i < count; i++)
            {
                var statType = ReadEnum<StatType>(cursor, "характеристика вещи");
                var min = cursor.Next();
                var max = cursor.Next();
                var value = cursor.Next();
                stats[i] = (statType, min, max, value);
            }
            return new CustomStatCombination(type, stats);
        }

        var pool = type == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();

        // Раньше неверный индекс молча заменялся ближайшим — теперь это ошибка ссылки.
        if (index < 0 || index >= pool.Length)
            throw new BuildCodeFormatException($"Неизвестная комбинация характеристик с номером {index}.");

        return pool[index];
    }

    private static bool TryGetCombinationIndex(StatCombination combination, out int index)
    {
        var pool = combination.StatCombinationType == StatCombinationType.ThreeStats
            ? StatCombinationsList.ThreeStatCombinations()
            : StatCombinationsList.FourStateCombinations();

        for (var i = 0; i < pool.Length; i++)
        {
            if (StatTypesMatch(pool[i], combination))
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
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

    private static int GetId(object? item, (int Id, Type Type)[] table, string kind)
    {
        if (item == null)
            return 0;

        foreach (var entry in table)
        {
            if (entry.Type == item.GetType())
                return entry.Id;
        }

        throw new InvalidOperationException(
            $"Для {kind} «{item.GetType().Name}» нет постоянного номера в BuildCode — добавьте его в таблицу.");
    }

    private static Elixir? ReadElixir(Cursor cursor, int version, Elixir[] pool, string what)
    {
        var raw = cursor.Next();

        if (version == 1)
        {
            // Версия 1: номер — позиция в списке эликсиров, -1 — нет эликсира.
            if (raw == -1)
                return null;
            if (raw < 0 || raw >= pool.Length)
                throw new BuildCodeFormatException($"Неизвестный {what} с номером {raw}.");
            return pool[raw];
        }

        // Версия 2: постоянный номер эликсира, 0 — нет эликсира.
        if (raw == 0)
            return null;

        foreach (var entry in ElixirIds)
        {
            if (entry.Id != raw)
                continue;

            var elixir = pool.FirstOrDefault(e => e.GetType() == entry.Type);
            if (elixir == null)
                throw new BuildCodeFormatException($"Эликсир с номером {raw} не подходит как {what}.");
            return elixir;
        }

        throw new BuildCodeFormatException($"Неизвестный {what} с номером {raw}.");
    }

    private static void DecodeAmplifications(Cursor cursor, int version, Character result)
    {
        var count = cursor.NextCount(MaxAmplifications, "число усилений");

        for (var i = 0; i < count; i++)
        {
            if (version == 1)
            {
                // Версия 1: только флаги «включено», по порядку в списке усилений.
                var usedFlag = cursor.Next() == 1;
                if (i < result.AvailableAmplifications.Length)
                    result.AvailableAmplifications[i].Used = usedFlag;
                continue;
            }

            var id = cursor.Next();
            var used = cursor.Next() == 1;

            Type? type = null;
            foreach (var entry in AmplificationIds)
            {
                if (entry.Id == id)
                    type = entry.Type;
            }

            if (type == null)
                throw new BuildCodeFormatException($"Неизвестное усиление с номером {id}.");

            var amplification = result.AvailableAmplifications.FirstOrDefault(a => a.GetType() == type);
            if (amplification != null)
                amplification.Used = used;
        }
    }

    private static T ReadEnum<T>(Cursor cursor, string what) where T : struct, Enum
    {
        return ToEnum<T>(cursor.Next(), what);
    }

    private static T ToEnum<T>(int raw, string what) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), raw))
            throw new BuildCodeFormatException($"Неизвестное значение {raw} для «{what}».");
        return (T)Enum.ToObject(typeof(T), raw);
    }

    // -1 — характеристика не выбрана.
    private static StatType? ReadOptionalStatType(Cursor cursor, string what)
    {
        var raw = cursor.Next();
        return raw == -1 ? null : ToEnum<StatType>(raw, what);
    }

    private sealed class BuildCodeFormatException : Exception
    {
        public BuildCodeFormatException(string message) : base(message)
        {
        }
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
                throw new BuildCodeFormatException("Код билда слишком короткий — ссылка обрезана или повреждена.");
            return _values[_index++];
        }

        // Читает количество элементов и проверяет, что оно разумное.
        public int NextCount(int max, string what)
        {
            var count = Next();
            if (count < 0 || count > max)
                throw new BuildCodeFormatException($"Недопустимое значение {count} для «{what}».");
            return count;
        }
    }
}
