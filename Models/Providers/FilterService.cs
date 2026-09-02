using Models.Stats;

namespace Models.Providers;

public static class FilterService
{
    public static IEnumerable<StatCombination> FilterStatCombinations(StatCombination[] statCombinations, string filter)
    {
        if (string.IsNullOrEmpty(filter))
            return statCombinations;

        var filters = filter.Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
        return statCombinations
            .Where(statCombination =>
                filters.All(f => statCombination.ToString().Contains(f, StringComparison.InvariantCultureIgnoreCase)));
    }
}