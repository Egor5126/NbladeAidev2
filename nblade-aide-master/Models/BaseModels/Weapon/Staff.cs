using Models.Stats;

namespace Models.BaseModels.Weapon;

public class Staff : WeaponBase
{
    public Staff(CharacterClass characterClass, WeaponOrder weaponOrder)
        : this(
            StatCombination.GetRandomStatCombination(),
            characterClass,
            StuffRuneOfImprovementLevel.Level12,
            weaponOrder,
            null)
    {
    }

    public Staff(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        WeaponOrder weaponOrder,
        List<WeaponGem>? weaponGems)
        : base(statCombination,
            characterClass,
            WeaponHandedClass.TwoHandedWeapon,
            WeaponType.Staff,
            runeOfImprovementLevel,
            weaponOrder,
            weaponGems)
    {
        if (characterClass != CharacterClass.Wizard)
        {
            throw new ArgumentException("Wrong class for weapon");
        }
    }

    public override WeaponBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null)
    {
        return new Staff(
            newStatCombination ?? StatCombination,
            CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            WeaponOrder,
            WeaponGems);
    }
}