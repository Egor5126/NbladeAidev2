using Models.Stats;

namespace Models.TalentsModels;

public class WarriorTalents : Talents
{
    public WarriorTalents()
    {
        Branch1 = new TalentBranch("Стойкость", new[]
        {
            (StatType.Health, 1),
            (StatType.CriticalDamageResistance, 0.75),
            (StatType.Block, 0.5),
        });
        Branch2 = new TalentBranch("Проворство", new[]
        {
            (StatType.Accuracy, 0.5),
            (StatType.Evasion, 1.0),
            (StatType.AttackSpeed, 1.0),
        });
        Branch3 = new TalentBranch("Ярость", new[]
        {
            (StatType.CriticalDamage, 0.75),
            (StatType.AttackPower, 0.5),
            (StatType.MagicalResistance, 1.0),
        });
    }

    public sealed override TalentBranch Branch3 { get; set; }
    public sealed override TalentBranch Branch2 { get; set; }
    public sealed override TalentBranch Branch1 { get; set; }
}