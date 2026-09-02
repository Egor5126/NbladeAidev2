using Models.Stats;

namespace Models.TalentsModels;

public class WizardTalents : Talents
{
    public WizardTalents()
    {
        Branch1 = new TalentBranch("Лёд", new[]
        {
            (StatType.Armor, 1.25),
            (StatType.CriticalDamageResistance, 0.75),
            (StatType.Accuracy, 0.5),
        });
        Branch2 = new TalentBranch("Пламя", new[]
        {
            (StatType.AttackPower, 0.75),
            (StatType.CriticalDamage, 0.75),
            (StatType.Health, 0.5),
        });
        Branch3 = new TalentBranch("Энергия", new[]
        {
            (StatType.CriticalDamage, 0.75),
            (StatType.Energy, 0.5),
            (StatType.Evasion, 0.75),
        });
    }

    public sealed override TalentBranch Branch3 { get; set; }
    public sealed override TalentBranch Branch2 { get; set; }
    public sealed override TalentBranch Branch1 { get; set; }
}