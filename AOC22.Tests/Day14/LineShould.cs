using System.Collections.Generic;
using System.Linq;
using AOC22.Day14;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day14;

public class LineShould
{
    [Fact]
    public void RepresentALine()
    {
        Point point1 = new(498, 4);
        Point point2 = new(498, 6);

        new Line("498,4 -> 498,6")
            .Should()
            .BeEquivalentTo(new Line(point1, point2));
    }

    [Fact]
    public void ToStringShouldRepresentTwoPointsConnected()
    {
        const string expected = "498,4 -> 498,6";
        new Line(expected).ToString().Should().Be(expected);
    }

    [Fact]
    public void ReturnContainedPointInBetweenTheStartAndEndOfAVerticalLine()
    {
        Point start = new(498, 4);
        Point end = new(498, 6);
        Line line = new(start, end);

        line.ContainedPoints
            .Should()
            .BeEquivalentTo(new List<Point>
            {
                new(498, 5)
            }
            .AsEnumerable());
    }

    [Fact]
    public void ReturnContainedPointInBetweenTheStartAndEndOfAHorizontalLine()
    {
        Point start = new(502, 9);
        Point end = new(494, 9);
        Line line = new(start, end);

        line.ContainedPoints
            .Should()
            .BeEquivalentTo(new List<Point>
                {
                    new(501, 9),
                    new(500, 9),
                    new(499, 9),
                    new(498, 9),
                    new(497, 9),
                    new(496, 9),
                    new(495, 9)
                }
                .AsEnumerable());
    }
}