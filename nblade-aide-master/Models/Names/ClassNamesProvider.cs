using Models.BaseModels;

namespace Models.Names;

public static class ClassNamesProvider
{
    public static string GetClassName(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Warrior => "Воин",
            CharacterClass.Bandit => "Разбойник",
            CharacterClass.Priest => "Жрец",
            CharacterClass.Archer => "Лучник",
            CharacterClass.Wizard => "Маг",
            _ => throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null)
        };
    }
}