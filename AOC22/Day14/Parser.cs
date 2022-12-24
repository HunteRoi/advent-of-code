namespace AOC22.Day14;

public static class Parser
{
    public static IEnumerable<Path> Parse(string input) 
        => input
            .Split('\n')
            .Select(path => new Path(path));
}