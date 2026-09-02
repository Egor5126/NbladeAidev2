using Models.Stats;

namespace Models.BaseModels.Armors;

public class Helmet : ArmorBase
{
    public Helmet(CharacterClass characterClass)
        : base(characterClass, ArmorType.Helmet)
    {
    }

    public Helmet(
        StatCombination statCombination,
        CharacterClass characterClass,
        StuffRuneOfImprovementLevel runeOfImprovementLevel,
        Gem gem1,
        Gem gem2)
        : base(statCombination, characterClass, ArmorType.Helmet, runeOfImprovementLevel, gem1, gem2)
    {
    }

    public override ArmorBase CreateFromInstance(
        StatCombination? newStatCombination = null,
        StuffRuneOfImprovementLevel? newRuneLevel = null,
        CharacterClass? characterClass = null)
    {
        return new Helmet(
            newStatCombination ?? StatCombination,
            characterClass ?? CharacterClass,
            newRuneLevel ?? RuneOfImprovementLevel,
            Gem1,
            Gem2);
    }
}