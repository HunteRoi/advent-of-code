using System.Drawing;
using System.Text;

namespace AOC22.Day7;

public sealed class FileSystemEntry
{
    private const string DirType = "dir";
    private const string FileType = "file";

    private readonly int? _size;

    private FileSystemEntry(string type, string name, int? size = null)
    {
        Type = type;
        Name = name;
        _size = size;
        Children = new List<FileSystemEntry>();
    }

    private string Type { get; }

    public string Name { get; }

    public IList<FileSystemEntry> Children { get; }

    public FileSystemEntry? Parent { get; private set; }
    
    public int Size => _size ?? Children.Sum(child => child.Size);
    
    public FileSystemEntry AddChild(FileSystemEntry entry)
    {
        if (Type != DirType) return this;
        
        entry.Parent = this;
        Children.Add(entry);

        return this;
    }
    
    public bool IsDirectory() => Type == DirType;

    public override string ToString()
    {
        return $"{Name} ({Type}{(_size.HasValue ? $", size={_size}" : "")})";
    }

    public string ToTree(int offset = 0)
    {
        var builder = new StringBuilder();

        var value = "- " + ToString();
        builder.AppendLine(value.PadLeft(value.Length + offset));
        foreach (var child in Children)
        {
            builder.AppendLine(child.ToTree(offset + 2));
        }
        
        return builder.ToString().TrimEnd();
    }

    public IEnumerable<FileSystemEntry> DirectoriesOfMaxSize(int maxSize)
    {
        var directories = new List<FileSystemEntry>();
        if (!IsDirectory()) return directories;

        if (Size <= maxSize) directories.Add(this);
        foreach (var child in Children)
        {
            directories.AddRange(child.DirectoriesOfMaxSize(maxSize));
        }
        return directories;
    }
    
    public IEnumerable<FileSystemEntry> DirectoriesOfMinSize(int minSize)
    {
        var directories = new List<FileSystemEntry>();
        if (!IsDirectory()) return directories;

        if (Size >= minSize) directories.Add(this);
        foreach (var child in Children)
        {
            directories.AddRange(child.DirectoriesOfMinSize(minSize));
        }
        return directories;
        
    }
    
    public static FileSystemEntry Folder(string name)
    {
        return new FileSystemEntry(DirType, name);
    }

    public static FileSystemEntry File(string name, int size)
    {
        return new FileSystemEntry(FileType, name, size);
    }
}
