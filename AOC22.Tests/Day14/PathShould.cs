using AOC22.Day14;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day14;

public class PathShould
{
    [Fact]
    public void RepresentTwoLinesWithOneCommonPoint()
    {
        const string commonPoint = "498,6";
        Line line1 = new($"498,4 -> {commonPoint}");
        Line line2 = new($"{commonPoint} -> 496,6");

        new Path($"498,4 -> {commonPoint} -> 496,6")
            .Should()
            .BeEquivalentTo(new Path(line1, line2));
    }

    [Fact]
    public void RepresentAPathWithMultipleCommonPoints()
    {
        const string firstCommonPoint = "502,4";
        const string secondCommonPoint = "502,9";
        Line line1 = new($"503,4 -> {firstCommonPoint}");
        Line line2 = new($"{firstCommonPoint} -> {secondCommonPoint}");
        Line line3 = new($"{secondCommonPoint} -> 494,9");
        
        new Path($"503,4 -> {firstCommonPoint} -> {secondCommonPoint} -> 494,9")
            .Should()
            .BeEquivalentTo(new Path(line1, line2, line3));
    }

    [Fact]
    public void ToStringShouldRepresentTwoLinesConnected()
    {
        const string firstCommonPoint = "502,4";
        const string secondCommonPoint = "502,9";
        const string expected = $"503,4 -> {firstCommonPoint} -> {secondCommonPoint} -> 494,9";
        
        new Path(expected).ToString().Should().Be(expected);
    }
}
