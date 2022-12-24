namespace AOC22.Day14;

public class Point
{
    private const string Separator = ",";
    
    public Point(string position)
        : this(position.Split(Separator)[0], position.Split(Separator)[1])
    { }

    private Point(string x, string y) 
        : this(int.Parse(x), int.Parse(y)) 
    { }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public override string ToString()
    {
        return $"{X}{Separator}{Y}";
    }
}