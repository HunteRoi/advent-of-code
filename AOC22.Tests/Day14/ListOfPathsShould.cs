using AOC22.Day14;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day14;

public class ListOfPathsShould
{
    [Fact]
    public void BeDrawnAsAProperScan()
    {
        const string expected = "  4     5  5\n  9     0  0\n  4     0  3\n0 ......+...\n1 ..........\n2 ..........\n3 ..........\n4 ....#...##\n5 ....#...#.\n6 ..###...#.\n7 ........#.\n8 ........#.\n9 #########.";
        const string input = "498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9";

        Parser
            .Parse(input)
            .ToString<Path>()
            .Should()
            .Be(expected);
    }
}