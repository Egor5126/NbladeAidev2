using Models.Stats;

namespace Models.BaseModels.Armors;

public class Belt : ArmorBase
{
    public Belt(CharacterClass characterClass)
        : base(characterClass, ArmorType.Belt)
    {
    }

    public Belt(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        ConcentrationGem gem2)
        : base(statCombination, characterClass, ArmorType.Belt, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Belt(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}