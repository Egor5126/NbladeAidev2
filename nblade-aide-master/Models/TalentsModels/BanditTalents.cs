using Models.Stats;

namespace Models.TalentsModels;

public class BanditTalents : Talents
{
    public BanditTalents()
    {
        Branch1 = new TalentBranch("Приемы", new[]
        {
            (StatType.Accuracy, 0.5),
            (StatType.CriticalDamageResistance, 0.75),
            (StatType.MagicalResistance, 0.75),
        });
        Branch2 = new TalentBranch("Уловки", new[]
        {
            (StatType.Energy, 0.75),
            (StatType.Evasion, 1.0),
            (StatType.CriticalDamage, 0.5),
        });
        Branch3 = new TalentBranch("Удары", new[]
        {
            (StatType.AttackPower, 0.75),
            (StatType.Parry, 0.5),
            (StatType.CriticalDamage, 0.5),
        });
    }

    public sealed override TalentBranch Branch3 { get; set; }
    public sealed override TalentBranch Branch2 { get; set; }
    public sealed override TalentBranch Branch1 { get; set; }
}