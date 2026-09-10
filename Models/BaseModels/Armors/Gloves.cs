using Models.Stats;

namespace Models.BaseModels.Armors;

public class Gloves : ArmorBase
{
    public Gloves(CharacterClass characterClass)
        : base(characterClass, ArmorType.Gloves)
    {
    }

    public Gloves(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        ConcentrationGem gem2)
        : base(statCombination, characterClass, ArmorType.Gloves, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Gloves(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}