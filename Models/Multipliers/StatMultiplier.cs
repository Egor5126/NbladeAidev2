namespace Models.Multipliers;

public static class StatMultiplier
{
    public static double ThreeStatMultiplier(int statOrder)
    {
        return statOrder switch
        {
            1 => 1,
            2 => 0.6,
            3 => 0.4,
            _ => throw new ArgumentOutOfRangeException(nameof(statOrder), statOrder, null)
        };
    }

    public static double FourStatMultiplier(int statOrder)
    {
        return statOrder switch
        {
            1 => 1,
            2 => 0.76,
            3 => 0.5,
            4 => 0.5,
            _ => throw new ArgumentOutOfRangeException(nameof(statOrder), statOrder, null)
        };
    }
}