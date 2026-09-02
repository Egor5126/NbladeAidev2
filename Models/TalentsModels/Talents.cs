namespace Models.TalentsModels;

public abstract class Talents
{
    public abstract TalentBranch Branch1 { get; set; }
    public abstract TalentBranch Branch2 { get; set; }
    public abstract TalentBranch Branch3 { get; set; }
    public int AvailablePoints { get; set; } = 42;
}