namespace AOC22.Day7;

public static class Runner22
{
    public static void Run()
    {
        var input = File.ReadAllText("./Day7/input.txt");
        var fileSystem = Parser.Parse(input);

        var sum = SumOfAllDirectoriesSizeOfAtMost100000(fileSystem);
        Console.WriteLine("Total size of all directories with size of at most 100_000: "+sum);

        var min = GetSmallestDirectorySizeToDelete(fileSystem);
        Console.WriteLine("Smallest directory's size to delete to have enough space for the update: "+min);
    }

    private static int GetSmallestDirectorySizeToDelete(FileSystemEntry fileSystem)
    {
        const int totalAvailableSize = 70_000_000;
        const int neededSizeForUpdate = 30_000_000;
        var totalUnusedSize = totalAvailableSize - fileSystem.Size;
        var minSizeNeededForUpdate = neededSizeForUpdate - totalUnusedSize;
        var directories = fileSystem.DirectoriesOfMinSize(minSizeNeededForUpdate);
        return directories.Min(dir => dir.Size);
    }

    private static int SumOfAllDirectoriesSizeOfAtMost100000(FileSystemEntry fileSystem)
    {
        const int maxSize = 100_000;
        var directories = fileSystem.DirectoriesOfMaxSize(maxSize);
        return directories.Sum(dir => dir.Size);
    }
}
