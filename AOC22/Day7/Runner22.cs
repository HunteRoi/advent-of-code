namespace AOC22.Day7;

public static class Runner22
{
    public static void Run()
    {
        const int maxSize = 100_000;
        var input = File.ReadAllText("./Day7/input.txt");
        
        var fileSystem = Parser.Parse(input);

        var directories = fileSystem.DirectoriesOfMaxSize(maxSize);
        var sum = directories.Sum(dir => dir.Size);
        
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}
