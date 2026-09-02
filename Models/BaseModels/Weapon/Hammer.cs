using Models.Stats;

namespace Models.BaseModels.Weapon;

public class Hammer : WeaponBase
{
    public Hammer(CharacterClass characterClass, WeaponOrder weaponOrder)
        : this(
            StatCombination.GetRandomStatCombination(),
            characterClass,
            StuffRuneOfImprovementLevel.Level12,
            weaponOrder,
            null)
    {
    }

    public Hammer(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        WeaponOrder weaponOrder,
        List<WeaponGem>? weaponGems)
        : base(statCombination,
            characterClass,
            WeaponHandedClass.TwoHandedWeapon,
            WeaponType.Hammer,
            runeOfImprovementLevel,
            weaponOrder,
            weaponGems)
    {
        if (characterClass != CharacterClass.Priest)
        {
            throw new ArgumentException("Wrong class for weapon");
        }
    }

    public override WeaponBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null)
    {
        return new Hammer(
            newStatCombination ?? StatCombination,
            CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            WeaponOrder,
            WeaponGems);
    }
}