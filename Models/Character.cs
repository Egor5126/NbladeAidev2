using Models.BaseModels;
using Models.BaseModels.Amplification;
using Models.BaseModels.Armors;
using Models.BaseModels.Cloak;
using Models.BaseModels.Jewelry;
using Models.BaseModels.Weapon;
using Models.Multipliers;
using Models.Stats;

namespace Models;

public class Character
{
    public Character(CharacterClass characterClass)
    {
        var weapons = GetDefaultWeapons(characterClass);

        CharacterClass = characterClass;
        Cloak = new Cloak();
        Amulet1 = new Amulet();
        Amulet2 = new Amulet();
        Necklace = new Necklace();
        Ring1 = new Ring();
        Ring2 = new Ring();
        Helmet = new Helmet(characterClass);
        ShoulderPad = new ShoulderPad(characterClass);
        Cuirass = new Cuirass(characterClass);
        Highway = new Highway(characterClass);
        Gloves = new Gloves(characterClass);
        Bracers = new Bracers(characterClass);
        Shoes = new Shoes(characterClass);
        Belt = new Belt(characterClass);
        MainWeapon = weapons.mainWeapon;
        SecondWeapon = weapons.secondWeapon;
        BaseClassStats = StatNumberProvider.GetBaseStats(characterClass);

        AttackElixir = null;
        DefenseElixir = null;
        AvailableAmplifications = new Amplification[]
        {
            new TrainedFighters(),
            new UniversalSoldiers(),
        };
    }

    public CharacterClass CharacterClass { get; set; }

    // Аура действует только у воина; у других классов значение игнорируется.
    public WarriorAura WarriorAura { get; set; } = WarriorAura.None;
    public Stat[] BaseClassStats { get; set; }

    public Cloak Cloak { get; set; }

    public Amulet Amulet1 { get; set; }
    public Amulet Amulet2 { get; set; }
    public Necklace Necklace { get; set; }
    public Ring Ring1 { get; set; }
    public Ring Ring2 { get; set; }

    public Helmet Helmet { get; set; }
    public ShoulderPad ShoulderPad { get; set; }
    public Cuirass Cuirass { get; set; }
    public Highway Highway { get; set; }
    public Gloves Gloves { get; set; }
    public Bracers Bracers { get; set; }
    public Shoes Shoes { get; set; }
    public Belt Belt { get; set; }

    public WeaponBase MainWeapon { get; set; }
    public WeaponBase? SecondWeapon { get; set; }

    public Elixir? AttackElixir { get; set; }
    public Elixir? DefenseElixir { get; set; }
    public Amplification[] AvailableAmplifications { get; set; }

    public (WeaponBase mainWeapon, WeaponBase? secondWeapon) GetDefaultWeapons(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => (new Blade(characterClass, WeaponOrder.Main), new Shield(characterClass, WeaponOrder.Second)),
            CharacterClass.Bandit => (new Dagger(characterClass, WeaponOrder.Main), new Dagger(characterClass, WeaponOrder.Second)),
            CharacterClass.Priest => (new Mint(characterClass, WeaponOrder.Main), new Shield(characterClass, WeaponOrder.Second)),
            CharacterClass.Archer => (new Bow(characterClass, WeaponOrder.Main), null),
            CharacterClass.Wizard => (new Staff(characterClass, WeaponOrder.Main), null),
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }

    public Dictionary<StatType, int> CalculateAllStats()
    {
        return CalculateBreakdown().ToDictionary(pair => pair.Key, pair => pair.Value.Total);
    }

    // Считает итоговые характеристики и показывает, из чего каждая сложилась:
    // класс, вещи, камни, эликсиры и усиления. Сумма частей равна итоговому значению.
    public Dictionary<StatType, StatBreakdown> CalculateBreakdown()
    {
        var statTypes = Enum.GetValues<StatType>();
        var result = statTypes.ToDictionary(statType => statType, _ => new StatBreakdown());

        // Значения до эликсиров (но после усилений гильдии). Проценты камней (концентрации и оружейных)
        // считаются именно от них, чтобы эликсир не увеличивал бонус от камней.
        var totalsBeforeElixirs = statTypes.ToDictionary(statType => statType, _ => 0);

        foreach (var stat in GetClassStats())
            AddMultipliedStat(stat, true, result, totalsBeforeElixirs);

        foreach (var stat in GetGearMultipliedStats())
            AddMultipliedStat(stat, false, result, totalsBeforeElixirs);

        // Белые статы бижутерии — часть вещей, но усиления и эликсиры на них не действуют.
        foreach (var stat in GetJewelryWhiteStats())
        {
            result[stat.StatType].Gear += stat.StatNumber;
            totalsBeforeElixirs[stat.StatType] += stat.StatNumber;
        }

        // Камни силы дают фиксированное значение, усиления и эликсиры на него тоже не действуют.
        foreach (var stat in GetPowerStoneStats())
        {
            result[stat.StatType].Gems += stat.StatNumber;
            totalsBeforeElixirs[stat.StatType] += stat.StatNumber;
        }

        // Камни концентрации (и оружейные) дают прибавку в процентах от значения без эликсиров.
        foreach (var multiplier in GetGemPercentMultipliers())
        {
            var bonus = (int)Math.Floor(totalsBeforeElixirs[multiplier.Key] * multiplier.Value);
            result[multiplier.Key].Gems += bonus;
        }

        ApplyWarriorAura(result);

        return result;
    }

    // Аура воина считается последней: проценты — от итога со всеми вещами, камнями и эликсирами.
    private void ApplyWarriorAura(Dictionary<StatType, StatBreakdown> result)
    {
        if (CharacterClass != CharacterClass.Warrior || WarriorAura == WarriorAura.None)
            return;

        foreach (var (statType, percent) in WarriorAuraProvider.GetPercentBonuses(WarriorAura))
            result[statType].Aura += (int)Math.Floor(result[statType].Total * percent);

        foreach (var (statType, value) in WarriorAuraProvider.GetFlatBonuses(WarriorAura))
            result[statType].Aura += value;
    }

    private void AddMultipliedStat(
        Stat stat,
        bool isClassStat,
        Dictionary<StatType, StatBreakdown> result,
        Dictionary<StatType, int> totalsBeforeElixirs)
    {
        var original = stat.StatNumber;

        UseAmplificationMultipliers(stat);
        totalsBeforeElixirs[stat.StatType] += stat.StatNumber;

        UseElixirMultiplier(AttackElixir, stat);
        UseElixirMultiplier(DefenseElixir, stat);

        var entry = result[stat.StatType];
        if (isClassStat)
            entry.Base += original;
        else
            entry.Gear += original;
        entry.Boost += stat.StatNumber - original;
    }

    private void UseAmplificationMultipliers(Stat stat)
    {
        foreach (var amplification in AvailableAmplifications)
        {
            if (!amplification.Used)
                continue;

            foreach (var increase in amplification.StatTypesToIncrease)
            {
                if (increase.StatType != stat.StatType)
                    continue;

                stat.UseMultiplier(increase.BaseMultiplier);
                break;
            }
        }
    }

    private void UseElixirMultiplier(Elixir? elixir, Stat multipliedStat)
    {
        if (elixir == null || elixir.StatTypesToIncrease.All(stat => stat.StatType != multipliedStat.StatType))
            return;

        var multiplier = elixir.StatTypesToIncrease
            .First(stat => stat.StatType == multipliedStat.StatType).BaseMultiplier;
        multipliedStat.UseMultiplier(multiplier);
    }

    private Stat[] GetClassStats()
    {
        return CopyStats(BaseClassStats);
    }

    private Stat[] GetGearMultipliedStats()
    {
        // TODO check getstats
        return CopyStats(Cloak.GetStats()
            .Concat(Amulet1.GetMultipliedStats())
            .Concat(Amulet2.GetMultipliedStats())
            .Concat(Necklace.GetMultipliedStats())
            .Concat(Ring1.GetMultipliedStats())
            .Concat(Ring2.GetMultipliedStats())
            .Concat(Helmet.GetMultipliedStats())
            .Concat(ShoulderPad.GetMultipliedStats())
            .Concat(Cuirass.GetMultipliedStats())
            .Concat(Highway.GetMultipliedStats())
            .Concat(Gloves.GetMultipliedStats())
            .Concat(Bracers.GetMultipliedStats())
            .Concat(Shoes.GetMultipliedStats())
            .Concat(Belt.GetMultipliedStats())
            .Concat(MainWeapon.GetStats())
            .Concat(SecondWeapon?.GetStats() ?? Array.Empty<Stat>()));
    }

    // Копии, чтобы множители при расчёте не меняли сами вещи.
    private static Stat[] CopyStats(IEnumerable<Stat> stats)
    {
        return stats.Select(stat => new Stat(stat.StatType, stat.StatNumber)).ToArray();
    }

    // Камни силы на броне.
    private IEnumerable<Stat> GetPowerStoneStats()
    {
        return Array.Empty<Stat>()
            .Concat(Helmet.GetNonMultipliedStats())
            .Concat(ShoulderPad.GetNonMultipliedStats())
            .Concat(Cuirass.GetNonMultipliedStats())
            .Concat(Highway.GetNonMultipliedStats())
            .Concat(Gloves.GetNonMultipliedStats())
            .Concat(Bracers.GetNonMultipliedStats())
            .Concat(Shoes.GetNonMultipliedStats())
            .Concat(Belt.GetNonMultipliedStats());
    }

    private IEnumerable<Stat> GetJewelryWhiteStats()
    {
        return Array.Empty<Stat>()
            .Concat(Amulet1.GetNonMultipliedStats())
            .Concat(Amulet2.GetNonMultipliedStats())
            .Concat(Necklace.GetNonMultipliedStats())
            .Concat(Ring1.GetNonMultipliedStats())
            .Concat(Ring2.GetNonMultipliedStats());
    }

    private Dictionary<StatType, double> GetGemPercentMultipliers()
    {
        var all = new Dictionary<StatType, double>(GetWeaponGemsMultipliers());
        foreach (var multiplier in GetConcentrationGemsMultipliers())
        {
            all[multiplier.Key] = all.TryGetValue(multiplier.Key, out var existing)
                ? existing + multiplier.Value
                : multiplier.Value;
        }

        return all;
    }

    private Dictionary<StatType, double> GetWeaponGemsMultipliers()
    {
        // Камни потенциала в игре сейчас отсутствуют — временно отключены (в том числе для билдов по ссылке).
        // Чтобы вернуть: раскомментировать блок ниже, убрать пустой return и раскомментировать UI
        // в WeaponComponent.razor и WeaponCompressedEditable.razor.
        return new Dictionary<StatType, double>();

        // var multipliers = new Dictionary<StatType, double>();
        // var weaponGems = MainWeapon.WeaponGems;
        // if (SecondWeapon != null)
        //     weaponGems = weaponGems.Concat(SecondWeapon.WeaponGems).ToList();
        // foreach (var weaponGem in weaponGems.Where(weaponGem => weaponGem.GemLevel != GemLevel.None))
        // {
        //     if (!multipliers.TryGetValue(weaponGem.StatType, out _))
        //         multipliers[weaponGem.StatType] = StatNumberProvider.GetWeaponGemMultiplier(weaponGem.GemLevel);
        //     else
        //         multipliers[weaponGem.StatType] += StatNumberProvider.GetWeaponGemMultiplier(weaponGem.GemLevel);
        // }
        //
        // return multipliers;
    }

    private Dictionary<StatType, double> GetConcentrationGemsMultipliers()
    {
        var multipliers = new Dictionary<StatType, double>();
        var concentrationGems = new[]
        {
            Helmet.Gem2,
            ShoulderPad.Gem2,
            Cuirass.Gem2,
            Highway.Gem2,
            Gloves.Gem2,
            Bracers.Gem2,
            Shoes.Gem2,
            Belt.Gem2,
        };

        foreach (var concentrationGem in concentrationGems.Where(gem => gem.GemLevel != ConcentrationStoneLevel.None && gem.StatType.HasValue))
        {
            var statType = concentrationGem.StatType!.Value;
            var value = StatNumberProvider.GetConcentrationStoneMultiplier(concentrationGem.GemLevel);
            if (!multipliers.TryGetValue(statType, out _))
                multipliers[statType] = value;
            else
                multipliers[statType] += value;
        }

        return multipliers;
    }

    public Character CreateFromCharacterClass(CharacterClass newCharacterClass)
    {
        return new Character(newCharacterClass);
    }
}