using System.Text.RegularExpressions;

namespace AOC22.Day7;

public static class GroupCollectionExtensions
{
    public static void Deconstruct(this GroupCollection groups, out string group1, out string group2, out string group3)
    {
        group1 = groups[1].Value;
        group2 = groups[2].Value;
        group3 = groups[3].Value;
    }
}