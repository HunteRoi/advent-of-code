using AOC22.Day14;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day14;

public class PointShould
{
    [Theory]
    [InlineData("498,4", 498, 4)]
    public void RepresentAPoint(string input, int x, int y) 
        => new Point(input)
            .Should()
            .BeEquivalentTo(new Point(x, y));

    [Fact]
    public void ToStringRepresentAPoint()
    {
        const string expected = "498,4";
        new Point(expected).ToString().Should().Be(expected);
    }
}