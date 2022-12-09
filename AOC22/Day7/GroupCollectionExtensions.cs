using System.Text.RegularExpressions;

namespace AOC22.Day7;

public static class GroupCollectionExtensions
{
    public static void Deconstruct(this GroupCollection groups, out string command, out string argument, out string output)
    {
        command = groups[1].Value;
        argument = groups[2].Value;
        output = groups[3].Value;
    }
}