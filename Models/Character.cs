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
        var initStatTypes = Enum.GetValues<StatType>()
            .Select(statType => new KeyValuePair<StatType, int>(statType, 0));
        var finalStats = new Dictionary<StatType, int>(initStatTypes);
        var multipliedStats = GetMultipliedStats();
        var nonMultipliedStats = GetNonMultipliedStats();

        foreach (var multipliedStat in multipliedStats)
        {
            foreach (var amplification in AvailableAmplifications)
            {
                if(amplification.Used == false
                   || amplification.StatTypesToIncrease.All(
                       stat => stat.StatType != multipliedStat.StatType))
                    continue;
                var multiplier = amplification.StatTypesToIncrease
                    .First(stat => stat.StatType == multipliedStat.StatType).BaseMultiplier;
                multipliedStat.UseMultiplier(multiplier);
            }

            UseElixirMultiplier(AttackElixir, multipliedStat);
            UseElixirMultiplier(DefenseElixir, multipliedStat);
        }

        var allStats = multipliedStats.Concat(nonMultipliedStats);
        foreach (var statGroup in allStats.GroupBy(e => e.StatType))
        {
            finalStats[statGroup.Key] = statGroup.Sum(stat => stat.StatNumber);
        }

        var weaponGemsMultipliers = GetWeaponGemsMultipliers();
        var concentrationGemsMultipliers = GetConcentrationGemsMultipliers();
        var allMultipliers = new Dictionary<StatType, double>(weaponGemsMultipliers);
        foreach (var multiplier in concentrationGemsMultipliers)
        {
            if (!allMultipliers.TryGetValue(multiplier.Key, out _))
                allMultipliers[multiplier.Key] = multiplier.Value;
            else
                allMultipliers[multiplier.Key] += multiplier.Value;
        }

        foreach (var multiplier in allMultipliers)
        {
            finalStats[multiplier.Key] = (int)Math.Floor(finalStats[multiplier.Key] * (multiplier.Value + 1));
        }

        return finalStats;
    }

    private void UseElixirMultiplier(Elixir? elixir, Stat multipliedStat)
    {
        if (elixir == null || elixir.StatTypesToIncrease.All(stat => stat.StatType != multipliedStat.StatType))
            return;

        var multiplier = elixir.StatTypesToIncrease
            .First(stat => stat.StatType == multipliedStat.StatType).BaseMultiplier;
        multipliedStat.UseMultiplier(multiplier);
    }

    private Stat[] GetMultipliedStats()
    {
        // TODO check getstats
        return BaseClassStats
            .Concat(Cloak.GetStats())
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
            .Concat(SecondWeapon?.GetStats() ?? Array.Empty<Stat>())
            .Select(stat => new Stat(stat.StatType, stat.StatNumber))
            .ToArray();
    }

    private IEnumerable<Stat> GetNonMultipliedStats()
    {
        return Array.Empty<Stat>()
            .Concat(Helmet.GetNonMultipliedStats())
            .Concat(ShoulderPad.GetNonMultipliedStats())
            .Concat(Cuirass.GetNonMultipliedStats())
            .Concat(Highway.GetNonMultipliedStats())
            .Concat(Gloves.GetNonMultipliedStats())
            .Concat(Bracers.GetNonMultipliedStats())
            .Concat(Shoes.GetNonMultipliedStats())
            .Concat(Belt.GetNonMultipliedStats())
            .Concat(Amulet1.GetNonMultipliedStats())
            .Concat(Amulet2.GetNonMultipliedStats())
            .Concat(Necklace.GetNonMultipliedStats())
            .Concat(Ring1.GetNonMultipliedStats())
            .Concat(Ring2.GetNonMultipliedStats());
    }

    private Dictionary<StatType, double> GetWeaponGemsMultipliers()
    {
        var multipliers = new Dictionary<StatType, double>();
        var weaponGems = MainWeapon.WeaponGems;
        if (SecondWeapon != null)
            weaponGems = weaponGems.Concat(SecondWeapon.WeaponGems).ToList();
        foreach (var weaponGem in weaponGems.Where(weaponGem => weaponGem.GemLevel != GemLevel.None))
        {
            if (!multipliers.TryGetValue(weaponGem.StatType, out _))
                multipliers[weaponGem.StatType] = StatNumberProvider.GetWeaponGemMultiplier(weaponGem.GemLevel);
            else
                multipliers[weaponGem.StatType] += StatNumberProvider.GetWeaponGemMultiplier(weaponGem.GemLevel);
        }
        
        return multipliers;
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