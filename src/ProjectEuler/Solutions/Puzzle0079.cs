using System.Text;
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

        var passcode = new StringBuilder();
        
        while (pointers.Count > 0)
        {
            var removeKey = '\0';
            
            foreach (var pointer in pointers)
            {
                if (pointer.Value.Count == 0)
                {
                    passcode.Append(pointer.Key);

                    removeKey = pointer.Key;
                }
            }

            pointers.Remove(removeKey);

            foreach (var pointer in pointers)
            {
                if (pointer.Value.Contains(removeKey))
                {
                    pointer.Value.Remove(removeKey);
                }
            }
        }

        return int.Parse(passcode.ToString()).ToString("N0");
    }
}