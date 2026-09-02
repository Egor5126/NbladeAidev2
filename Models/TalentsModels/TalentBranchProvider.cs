using Models.BaseModels;

namespace Models.TalentsModels;

public static class TalentBranchProvider
{
    public static Talents GetTalents(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => new WarriorTalents(),
            CharacterClass.Bandit => new BanditTalents(),
            CharacterClass.Priest => throw new ArgumentOutOfRangeException(),
            CharacterClass.Archer => new ArcherTalents(),
            CharacterClass.Wizard => new WizardTalents(),
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }
}