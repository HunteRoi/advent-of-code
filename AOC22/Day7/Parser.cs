using System.Text.RegularExpressions;

namespace AOC22.Day7;

public static class Parser
{
    public static FileSystemEntry Parse(string input)
    {
        var rootDirectory = FileSystemEntry.Folder("/");
        
        FileSystemEntry? currentDirectory = null;
        foreach (var inputWithOutput in input.Split("$ ").Skip(1))
        {
            var lines = inputWithOutput.Trim().Split("\n");
            var command = lines.First();
            var commandName = command.Split(" ").First();
            var arguments = command.Split(" ").Skip(1).ToArray();
            var outputs = lines.Skip(1);
            switch (commandName)
            {
                case "cd":
                    currentDirectory = arguments[0] switch
                    {
                        "/" => rootDirectory,
                        ".." => currentDirectory!.Parent,
                        _ => currentDirectory!.Children.First(child =>
                            child.IsDirectory() && child.Name == arguments[0])
                    };
                    break;
                case "ls":
                    foreach (var output in outputs) currentDirectory!.AddChild(ReadEntry(output));
                    break;
            }
        }
        
        return rootDirectory;
    }

    private static FileSystemEntry ReadEntry(string entry)
    {
        var entryMetadata = entry.Split(" ");
        return entryMetadata[0] == "dir" 
            ? FileSystemEntry.Folder(entryMetadata[1])
            : FileSystemEntry.File(entryMetadata[1], int.Parse(entryMetadata[0]));
    }
}
