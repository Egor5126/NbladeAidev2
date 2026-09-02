using Models.Stats;

namespace Models.BaseModels.Armors;

public class ShoulderPad : ArmorBase
{
    public ShoulderPad(CharacterClass characterClass)
        : base(characterClass, ArmorType.ShoulderPad)
    {
    }

    public ShoulderPad(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        Gem gem2)
        : base(statCombination, characterClass, ArmorType.ShoulderPad, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new ShoulderPad(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}