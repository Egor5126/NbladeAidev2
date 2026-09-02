using Models.Stats;

namespace Models.BaseModels.Weapon;

public class Blade : WeaponBase
{
    public Blade(CharacterClass characterClass, WeaponOrder weaponOrder)
        : this(
            StatCombination.GetRandomStatCombination(),
            characterClass,
            StuffRuneOfImprovementLevel.Level12,
            weaponOrder,
            null)
    {
    }

    public Blade(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        WeaponOrder weaponOrder,
        List<WeaponGem>? weaponGems)
        : base(statCombination,
            characterClass,
            WeaponHandedClass.OneHandedWeapon,
            WeaponType.Blade,
            runeOfImprovementLevel,
            weaponOrder,
            weaponGems)
    {
        if (characterClass != CharacterClass.Warrior && characterClass != CharacterClass.Bandit)
        {
            throw new ArgumentException("Wrong class for weapon");
        }
    }

    public override WeaponBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null)
    {
        return new Blade(
            newStatCombination ?? StatCombination,
            CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            WeaponOrder,
            WeaponGems);
    }
}