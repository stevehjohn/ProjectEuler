using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0079 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var pointers = new Dictionary<char, HashSet<char>>();
        
        foreach (var line in Input)
        {
            if (! pointers.ContainsKey(line[0]))
            {
                pointers.Add(line[0], new HashSet<char>());
            }

            for (var i = 1; i < 3; i++)
            {
                if (! pointers.ContainsKey(line[i]))
                {
                    pointers.Add(line[i], new HashSet<char>());
                }

                pointers[line[i]].Add(line[i - 1]);
            }
        }

        return "";
    }
}