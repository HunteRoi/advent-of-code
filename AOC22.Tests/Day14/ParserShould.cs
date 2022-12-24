using System.Collections.Generic;
using AOC22.Day14;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day14;

public class ParserShould
{
    [Fact]
    public void RepresentAListOfPaths()
    {
        Path firstPath = new("498,4 -> 498,6 -> 496,6");
        Path secondPath = new("503,4 -> 502,4 -> 502,9 -> 494,9");
        var input = $"{firstPath}\n{secondPath}";
        var expected = new List<Path>
        {
            firstPath,
            secondPath
        };
        Parser
            .Parse(input)
            .Should()
            .BeEquivalentTo(expected);
    }
}