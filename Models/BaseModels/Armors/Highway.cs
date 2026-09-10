using Models.Stats;

namespace Models.BaseModels.Armors;

public class Highway : ArmorBase
{
    public Highway(CharacterClass characterClass)
        : base(characterClass, ArmorType.Highway)
    {
    }

    public Highway(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        ConcentrationGem gem2)
        : base(statCombination, characterClass, ArmorType.Highway, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Highway(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}