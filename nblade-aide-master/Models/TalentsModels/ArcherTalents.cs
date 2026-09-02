using Models.Stats;

namespace Models.TalentsModels;

public class ArcherTalents : Talents
{
    public ArcherTalents()
    {
        Branch1 = new TalentBranch("Охотник", new[]
        {
            (StatType.Health, 0.75),
            (StatType.AttackSpeed, 0.5),
            (StatType.CriticalDamageResistance, 0.75),
        });
        Branch2 = new TalentBranch("Стрелок", new[]
        {
            (StatType.AttackPower, 0.75),
            (StatType.Accuracy, 0.5),
            (StatType.EnergyRestoration, 0.75),
        });
        Branch3 = new TalentBranch("Снайпер", new[]
        {
            (StatType.Accuracy, 0.75),
            (StatType.Energy, 0.5),
            (StatType.Evasion, 0.75),
        });
    }

    public sealed override TalentBranch Branch3 { get; set; }
    public sealed override TalentBranch Branch2 { get; set; }
    public sealed override TalentBranch Branch1 { get; set; }
}