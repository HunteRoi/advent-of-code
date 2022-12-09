using System.Collections.Generic;
using AOC22.Day7;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day7;

public class FileSystemEntryShould
{
    [Fact]
    public void PrintAsATree()
    {
        const string expected = "- / (dir)\r\n  - a (dir)\r\n    - e (dir)\r\n      - i (file, size=584)\r\n    - f (file, size=29116)\r\n    - g (file, size=2557)\r\n    - h.lst (file, size=62596)\r\n  - b.txt (file, size=14848514)\r\n  - c.dat (file, size=8504156)\r\n  - d (dir)\r\n    - j (file, size=4060174)\r\n    - d.log (file, size=8033020)\r\n    - d.ext (file, size=5626152)\r\n    - k (file, size=7214296)";
        var entry = FileSystemEntry.Folder("/")
            .AddChild(
                FileSystemEntry.Folder("a")
                    .AddChild(
                        FileSystemEntry.Folder("e")
                            .AddChild(FileSystemEntry.File("i", 584))
                    )
                    .AddChild(FileSystemEntry.File("f", 29_116))
                    .AddChild(FileSystemEntry.File("g", 2_557))
                    .AddChild(FileSystemEntry.File("h.lst", 62_596))
            )
            .AddChild(FileSystemEntry.File("b.txt", 14_848_514))
            .AddChild(FileSystemEntry.File("c.dat", 8_504_156))
            .AddChild(
                FileSystemEntry.Folder("d")
                    .AddChild(FileSystemEntry.File("j", 4_060_174))
                    .AddChild(FileSystemEntry.File("d.log", 8_033_020))
                    .AddChild(FileSystemEntry.File("d.ext", 5_626_152))
                    .AddChild(FileSystemEntry.File("k", 7_214_296))
            );
        
        var actual = entry.ToTree();
            
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ReturnMaxSized()
    {
        var entry = FileSystemEntry.Folder("/")
            .AddChild(
                FileSystemEntry.Folder("a")
                    .AddChild(
                        FileSystemEntry.Folder("e")
                            .AddChild(FileSystemEntry.File("i", 584))
                    )
                    .AddChild(FileSystemEntry.File("f", 29_116))
                    .AddChild(FileSystemEntry.File("g", 2_557))
                    .AddChild(FileSystemEntry.File("h.lst", 62_596))
            )
            .AddChild(FileSystemEntry.File("b.txt", 14_848_515))
            .AddChild(FileSystemEntry.File("c.dat", 8_504_156))
            .AddChild(
                FileSystemEntry.Folder("d")
                    .AddChild(FileSystemEntry.File("j", 4_060_174))
                    .AddChild(FileSystemEntry.File("d.log", 8_033_020))
                    .AddChild(FileSystemEntry.File("d.ext", 5_626_152))
                    .AddChild(FileSystemEntry.File("k", 7_214_296))
            );
        var expected = new List<FileSystemEntry>
        {
            entry.Children[0],
            entry.Children[0].Children[0]
        };

        var actual = entry.DirectoriesOfMaxSize(100_000);
        
        actual.Should().BeEquivalentTo(expected);
        // actual.Sum(c => c.Size).Should().Be(95_437);
    }
    
    [Fact]
    public void ReturnMinSized()
    {
        var entry = FileSystemEntry.Folder("/")
            .AddChild(
                FileSystemEntry.Folder("a")
                    .AddChild(
                        FileSystemEntry.Folder("e")
                            .AddChild(FileSystemEntry.File("i", 584))
                    )
                    .AddChild(FileSystemEntry.File("f", 29_116))
                    .AddChild(FileSystemEntry.File("g", 2_557))
                    .AddChild(FileSystemEntry.File("h.lst", 62_596))
            )
            .AddChild(FileSystemEntry.File("b.txt", 14_848_515))
            .AddChild(FileSystemEntry.File("c.dat", 8_504_156))
            .AddChild(
                FileSystemEntry.Folder("d")
                    .AddChild(FileSystemEntry.File("j", 4_060_174))
                    .AddChild(FileSystemEntry.File("d.log", 8_033_020))
                    .AddChild(FileSystemEntry.File("d.ext", 5_626_152))
                    .AddChild(FileSystemEntry.File("k", 7_214_296))
            );
        var expected = new List<FileSystemEntry>
        {
            entry,
            entry.Children[3]
        };

        var actual = entry.DirectoriesOfMinSize(8_381_165);
        
        actual.Should().BeEquivalentTo(expected);
        // actual.Min(c => c.Size).Should().Be(24_933_642);
    }
}