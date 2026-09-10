namespace Models.Stats;

// Комбинация характеристик, собранная игроком вручную: он сам выбирает, какие
// именно статы стоят на предмете, и вводит их итоговые (уже готовые) значения.
// В отличие от именованных комбинаций из StatCombinationsList, значения здесь
// никогда не пересчитываются множителями руны/редкости — что игрок ввёл,
// то и остаётся, при любом пересоздании вещи.
public sealed class CustomStatCombination : StatCombination
{
    private readonly Stat[] _stats;

    public CustomStatCombination(StatCombinationType statCombinationType, IEnumerable<Stat> stats)
    {
        StatCombinationType = statCombinationType;
        _stats = stats.Select(stat => new Stat(stat.StatType, stat.StatNumber)).ToArray();
    }

    public override StatCombinationType StatCombinationType { get; }
    public override string Name => string.Empty;
    public override Stat[] Stats => _stats;
    public override Stat[] OriginStats => _stats;

    // Ручные значения не должны меняться множителями руны/редкости — какое
    // значение ввёл игрок, такое и должно остаться.
    public override StatCombination UseMultiplier(double multiplier) => this;
    public override StatCombination ResetMultipliers() => this;

    public override string ToString() => string.Empty;
}
