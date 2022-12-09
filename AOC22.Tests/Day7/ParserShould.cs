using AOC22.Day7;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day7;

public class ParserShould
{
    [Fact]
    public void ParseCdAndReturnDirectory()
    {
        const string input = "$ cd /";
        const string expected = "- / (dir)";
        
        var result = Parser.Parse(input);
        
        result.ToTree().Should().Be(expected);
    }

    [Fact]
    public void ParseCdAndLsAndReturnDirectoryAndContent()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d";
        const string expected = "- / (dir)\r\n  - a (dir)\r\n  - b.txt (file, size=14848514)\r\n  - c.dat (file, size=8504156)\r\n  - d (dir)";

        var result = Parser.Parse(input);

        result.ToTree().Should().Be(expected);
    }

    [Fact]
    public void ParseWholeStructureAndReturnTree()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst";
        const string expected = "- / (dir)\r\n  - a (dir)\r\n    - e (dir)\r\n    - f (file, size=29116)\r\n    - g (file, size=2557)\r\n    - h.lst (file, size=62596)\r\n  - b.txt (file, size=14848514)\r\n  - c.dat (file, size=8504156)\r\n  - d (dir)";

        var result = Parser.Parse(input);

        result.ToTree().Should().Be(expected);
    }
}