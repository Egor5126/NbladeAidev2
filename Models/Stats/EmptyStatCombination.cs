namespace Models.Stats;

// Обозначает, что для вещи ещё не выбрана конкретная комбинация характеристик —
// то есть игрок ещё не указал, какие именно статы выпали на предмете.
// Не содержит статов и не участвует в списке для выбора (PotentialStatCombinations),
// поэтому появляется только как начальное состояние новой вещи.
public sealed class EmptyStatCombination : StatCombination
{
    public static readonly EmptyStatCombination ThreeStats = new(StatCombinationType.ThreeStats);
    public static readonly EmptyStatCombination FourStats = new(StatCombinationType.FourStats);

    private EmptyStatCombination(StatCombinationType statCombinationType)
    {
        StatCombinationType = statCombinationType;
    }

    public override StatCombinationType StatCombinationType { get; }
    public override string Name => string.Empty;
    public override Stat[] Stats { get; } = Array.Empty<Stat>();
    public override Stat[] OriginStats { get; } = Array.Empty<Stat>();

    public override string ToString() => string.Empty;

    public static EmptyStatCombination For(StatCombinationType type)
    {
        return type == StatCombinationType.ThreeStats ? ThreeStats : FourStats;
    }
}
