namespace AOC22.Day14;

public class Line
{
    private const string Separator = " -> ";

    public Line(string points)
        : this(new Point(points.Split(Separator)[0]), new Point(points.Split(Separator)[1]))
    { }
    
    public Line(Point point1, Point point2)
    {
        StartPoint = point1;
        EndPoint = point2;
    }

    public Point StartPoint { get; }

    public Point EndPoint { get; }

    private bool IsVertical => StartPoint.X - EndPoint.X != 0;
    
    public IEnumerable<Point> ContainedPoints
    {
        get
        { 
            var list = new List<Point>();
            var startingPosition = IsVertical ? StartPoint.X : StartPoint.Y;
            var endPosition = IsVertical ? EndPoint.X : EndPoint.Y;

            if (startingPosition > endPosition)
            {
                (endPosition, startingPosition) = (startingPosition, endPosition);
            }
            startingPosition += 1;
            
            for (var i = startingPosition; i < endPosition; i++)
            {
                list.Add(IsVertical ? new Point(i, StartPoint.Y) : new Point(StartPoint.X, i));    
            }
            return list;
        }
    }

    public override string ToString()
    {
        return $"{StartPoint}{Separator}{EndPoint}";
    }
}