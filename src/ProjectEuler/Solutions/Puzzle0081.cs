using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0081 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var matrix = ParseInput();

        return "?";
    }

    private int[,] ParseInput()
    {
        var matrix = new int[80, 80];

        var i = 0;
        
        foreach (var line in Input)
        {
            var parts = line.Split(',');

            for (var x = 0; x < 80; x++)
            {
                matrix[x, i] = int.Parse(parts[x]);
            }

            i++;
        }
        
        return matrix;
    }
}