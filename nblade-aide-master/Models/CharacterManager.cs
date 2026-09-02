using Models.BaseModels;

namespace Models;

public class CharacterManager
{
    public Character Character { get; set; } = new(CharacterClass.Wizard);
}