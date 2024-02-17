using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0011 : Puzzle
{
    private int[,] _matrix;
    
    public override string GetAnswer()
    {
        LoadInput();
        
        ParseInput();

        var max = 0;

        for (var x = 0; x < 20; x++)
        {
            for (var y = 0; y < 20; y++)
            {
                if (x < 17)
                {
                    max = Math.Max(max, _matrix[x, y] * _matrix[x + 1, y] * _matrix[x + 2, y] * _matrix[x + 3, y]);
                }

                if (y < 17)
                {
                    max = Math.Max(max, _matrix[x, y] * _matrix[x, y + 1] * _matrix[x, y + 2] * _matrix[x, y + 3]);
                }

                if (x < 17 && y < 17)
                {
                    max = Math.Max(max, _matrix[x, y] * _matrix[x + 1, y + 1] * _matrix[x + 2, y + 2] * _matrix[x + 3, y + 3]);
                }

                if (x < 17 && y > 2)
                {
                    max = Math.Max(max, _matrix[x, y] * _matrix[x + 1, y - 1] * _matrix[x + 2, y - 2] * _matrix[x + 3, y - 3]);
                }
            }
        }

        return max.ToString("N0");
    }

    private void ParseInput()
    {
        _matrix = new int[20, 20];

        var y = 0;
        
        foreach (var line in Input)
        {
            var parts = line.Split(' ').Select(int.Parse).ToList();

            var x = 0;
            
            foreach (var part in parts)
            {
                _matrix[x, y] = part;

                x++;
            }
            
            y++;
        }
    }
}