namespace Models.Stats;

// Числовые значения попадают в ссылку «Поделиться билдом» — не менять и не переставлять,
// новые элементы добавлять только в конец с новым номером.
public enum StatType
{
    AttackPower = 0,
    AttackSpeed = 1,
    Accuracy = 2,
    CriticalChance = 3,
    Armor = 4,
    MagicArmor = 5,
    MagicalResistance = 6,
    CriticalDamageResistance = 7,
    Evasion = 8,
    Block = 9,
    Parry = 10,
    CharacterSpeed = 11,
    Health = 12,
    HealthRestoration = 13,
    Energy = 14,
    EnergyRestoration = 15,
    CriticalDamage = 16,
}
