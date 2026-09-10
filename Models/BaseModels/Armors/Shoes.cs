using Models.Stats;

namespace Models.BaseModels.Armors;

public class Shoes : ArmorBase
{
    public Shoes(CharacterClass characterClass)
        : base(characterClass, ArmorType.Shoes)
    {
    }

    public Shoes(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        ConcentrationGem gem2)
        : base(statCombination, characterClass, ArmorType.Shoes, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Shoes(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}