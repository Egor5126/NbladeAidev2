using Models.Stats;

namespace Models.BaseModels.Weapon;

public class Mint : WeaponBase
{
    public Mint(CharacterClass characterClass, WeaponOrder weaponOrder)
        : this(
            StatCombination.GetRandomStatCombination(),
            characterClass,
            StuffRuneOfImprovementLevel.Level12,
            weaponOrder,
            null)
    {
    }

    public Mint(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        WeaponOrder weaponOrder,
        List<WeaponGem>? weaponGems)
        : base(statCombination,
            characterClass,
            WeaponHandedClass.OneHandedWeapon,
            WeaponType.Mint,
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
        return new Mint(
            newStatCombination ?? StatCombination,
            CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            WeaponOrder,
            WeaponGems);
    }
}