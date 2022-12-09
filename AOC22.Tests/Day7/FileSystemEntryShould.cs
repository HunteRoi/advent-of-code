using System.Collections.Generic;
using AOC22.Day7;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day7;

public class FileSystemEntryShould
{
    private readonly FileSystemEntry _fileSystemEntry;
    
    public FileSystemEntryShould()
    {
        _fileSystemEntry = FileSystemEntry.Folder("/").AddChildren(
            FileSystemEntry.Folder("a").AddChildren(
                FileSystemEntry.Folder("e").AddChild(
                    FileSystemEntry.File("i", 584)
                ),
                FileSystemEntry.File("f", 29_116),
                FileSystemEntry.File("g", 2_557),
                FileSystemEntry.File("h.lst", 62_596)
            ),
            FileSystemEntry.File("b.txt", 14_848_514),
            FileSystemEntry.File("c.dat", 8_504_156),
            FileSystemEntry.Folder("d").AddChildren(
                FileSystemEntry.File("j", 4_060_174),
                FileSystemEntry.File("d.log", 8_033_020),
                FileSystemEntry.File("d.ext", 5_626_152),
                FileSystemEntry.File("k", 7_214_296)
            )
        );
    }

    [Fact(DisplayName = "Given a file, it should return its size")]
    public void ReturnFileSize()
    {
        const int size = 584;
        var entry = FileSystemEntry.File("a", size);
        
        entry.Size.Should().Be(size);
    }

    [Fact(DisplayName = "Given a directory, it should return the sum of its files and directories recursively")]
    public void ReturnDirectoryFilesTotalSize()
    {
        const int totalSize = 94853;
        var entry = FileSystemEntry.Folder("a").AddChildren(
            FileSystemEntry.Folder("e").AddChild(
                FileSystemEntry.File("i", 584)
            ),
            FileSystemEntry.File("f", 29_116),
            FileSystemEntry.File("g", 2_557),
            FileSystemEntry.File("h.lst", 62_596)
        );
        
        entry.Size.Should().Be(totalSize);
    }
    
    [Fact(DisplayName = "Given a directory, it should return the directory and its content as a file system hierarchy tree")]
    public void ReturnDirectoriesAndFilesAsATree()
    {
        const string expected = "- / (dir)\n  - a (dir)\n    - e (dir)\n      - i (file, size=584)\n    - f (file, size=29116)\n    - g (file, size=2557)\n    - h.lst (file, size=62596)\n  - b.txt (file, size=14848514)\n  - c.dat (file, size=8504156)\n  - d (dir)\n    - j (file, size=4060174)\n    - d.log (file, size=8033020)\n    - d.ext (file, size=5626152)\n    - k (file, size=7214296)";
        
        var actual = _fileSystemEntry.ToTree();
            
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact(DisplayName = "Given a directory, it should return all the directories found which have their size of at most the provided maximum size")]
    public void ReturnAllDirectoriesWhichSizeIsOfMaximumProvidedSize()
    {
        var expected = new List<FileSystemEntry>
        {
            _fileSystemEntry.Directories[0],
            _fileSystemEntry.Directories[0].Directories[0]
        };

        var actual = _fileSystemEntry.DirectoriesOfMaxSize(100_000);
        
        actual.Should().BeEquivalentTo(expected);
        // actual.Sum(c => c.Size).Should().Be(95_437);
    }
    
    [Fact(DisplayName = "Given a directory, it should return all the directories found which have their size of at least the provided minimum size")]
    public void ReturnAllDirectoriesWhichSizeIsMinimumProvidedSize()
    {
        var expected = new List<FileSystemEntry>
        {
            _fileSystemEntry,
            _fileSystemEntry.Directories[1]
        };

        var actual = _fileSystemEntry.DirectoriesOfMinSize(8_381_165);
        
        actual.Should().BeEquivalentTo(expected);
        // actual.Min(c => c.Size).Should().Be(24_933_642);
    }
}