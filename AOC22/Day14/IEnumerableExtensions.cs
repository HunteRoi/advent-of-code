using System.Text;

namespace AOC22.Day14;

public static class IEnumerableExtensions
{
    private static readonly Point SandOrigin = new(500, 0);

    public static string ToString<T>(this IEnumerable<Path> paths) where T : Path
    {
        var points = paths
            .SelectMany(path => path.Lines)
            .SelectMany(line =>
            {
                List<Point> list = new() { line.StartPoint };
                list.AddRange(line.ContainedPoints);
                list.Add(line.EndPoint);
                return list;
            })
            .Distinct()
            .Append(SandOrigin);
        
        var minimumXPosition = points.Min(point => point.X);
        var maximumXPosition = points.Max(point => point.X);
        var maximumNumberOfLines = points.Max(point => point.Y);
        var minimumNumberOfLines = points.Min(point => point.Y);
        var maximumNumberOfColumns = maximumXPosition - minimumXPosition;

        StringBuilder builder = new();
        BuildHeaders(builder, minimumXPosition, maximumXPosition);
        BuildStrati(builder, maximumNumberOfLines, minimumNumberOfLines, maximumNumberOfColumns, minimumXPosition, points);

        return builder.ToString().TrimEnd();
    }

    private static void BuildStrati(StringBuilder builder, int maximumNumberOfLines, int minimumNumberOfLines,
        int maximumNumberOfColumns, int minimumNumberOfColumns, IEnumerable<Point> points)
    {
        for (var i = 0; i <= maximumNumberOfLines; i++)
        {
            builder.Append($"{i} ");
            for (var j = 0; j <= maximumNumberOfColumns; j++)
            {
                var yPosition = minimumNumberOfLines + i;
                var xPosition = minimumNumberOfColumns + j;
                var point = points.FirstOrDefault(p => p.X == xPosition && p.Y == yPosition);

                if (point == SandOrigin) builder.Append('+');
                else builder.Append(point == null ? '.' : '#');
            }
            builder.Append('\n');
        }
    }

    private static void BuildHeaders(StringBuilder builder, int minimumXPosition, int maximumXPosition)
    {
        var headers = new List<string>
        {
            minimumXPosition.ToString(), 
            SandOrigin.X.ToString(), 
            maximumXPosition.ToString()
        }.Distinct();
        var numberOfLines = headers.Max(h => h.Length);
        for (var characterPosition = 0; characterPosition < numberOfLines; characterPosition++)
        {
            builder.Append("  ");
            for (var currentXPosition = minimumXPosition; currentXPosition <= maximumXPosition; currentXPosition++)
            {
                builder.Append(headers.Contains(currentXPosition.ToString())
                    ? currentXPosition.ToString().Substring(characterPosition, 1)
                    : ' '
                );
            }

            builder.Append('\n');
        }
    }
}