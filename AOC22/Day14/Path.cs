using System.Text;

namespace AOC22.Day14;

public class Path
{
    private const string Separator = " -> ";
    
    private Path(LinkedList<Line> lines)
    {
        Lines = lines;
    }
    
    public Path(params Line[] lines)
        : this(new LinkedList<Line>(lines))
    { }
    
    public Path(string lines)
        : this(ToLinkedLines(lines)) 
    { }

    private static LinkedList<Line> ToLinkedLines(string linesAsString)
    {
        var linkedPoints = new LinkedList<Point>(linesAsString
            .Split(Separator)
            .Select(position => new Point(position)));

        var linkedLines = linkedPoints.Aggregate(new LinkedList<Line>(), (lines, currentPoint) =>
        {
            var nextPoint = linkedPoints.Find(currentPoint)?.Next;
            if (nextPoint != null)
            {
                lines.AddLast(new Line(currentPoint, nextPoint.Value));
            }
            return lines;
        });
        
        return linkedLines;
    }
    
    public LinkedList<Line> Lines { get; }

    public override string ToString()
    {
        return Lines
            .Aggregate(new StringBuilder(), (seed, line) =>
            {
                if (seed.Length != 0) seed.Append(Separator);
                
                seed.Append(line.StartPoint);
                if (Lines.Last?.ValueRef != line) return seed;
                
                seed.Append(Separator);
                seed.Append(line.EndPoint);
                
                return seed;
            })
            .ToString();
    }
}
