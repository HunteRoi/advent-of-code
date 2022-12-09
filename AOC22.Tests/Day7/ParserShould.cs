using AOC22.Day7;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day7;

public class ParserShould
{
    [Fact(DisplayName = "Given the \"cd /\" command, it should return a file system entry representing the root directory")]
    public void ParseCdAndReturnDirectory()
    {
        const string input = "$ cd /";
        var expected = FileSystemEntry.Folder("/");
        
        var result = Parser.Parse(input);
        
        result.Should().BeEquivalentTo(expected);
    }

    [Fact(DisplayName = "Given the \"cd\" and \"ls\" commands once, it should return a file system entry representing the root directory and its content of depth 1")]
    public void ParseCdAndLsAndReturnDirectoryAndContent()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d";
        var expected = FileSystemEntry.Folder("/").AddChildren(
            FileSystemEntry.Folder("a"),
            FileSystemEntry.File("b.txt", 14_848_514),
            FileSystemEntry.File("c.dat", 8_504_156),
            FileSystemEntry.Folder("d")
        );

        var result = Parser.Parse(input);

        result.Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
    }

    [Fact(DisplayName = "Given the \"cd\" and \"ls\" commands several times, it should return a file system entry representing the root directory and its content of depth 2")]
    public void ParseWholeStructureAndReturnTree()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst";
        var expected = FileSystemEntry.Folder("/").AddChildren(
            FileSystemEntry.Folder("a").AddChildren(
                FileSystemEntry.Folder("e"),
                FileSystemEntry.File("f", 29_116),
                FileSystemEntry.File("g", 2_557),
                FileSystemEntry.File("h.lst", 62_596)
            ),
            FileSystemEntry.File("b.txt", 14_848_514),
            FileSystemEntry.File("c.dat", 8_504_156),
            FileSystemEntry.Folder("d")
        );

        var result = Parser.Parse(input);

        result.Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
    }
}
