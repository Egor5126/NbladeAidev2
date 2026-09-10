using Models.Stats;

namespace Models.BaseModels.Armors;

public class Cuirass : ArmorBase
{
    public Cuirass(CharacterClass characterClass)
        : base(characterClass, ArmorType.Cuirass)
    {
    }

    public Cuirass(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        ConcentrationGem gem2)
        : base(statCombination, characterClass, ArmorType.Cuirass, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Cuirass(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}