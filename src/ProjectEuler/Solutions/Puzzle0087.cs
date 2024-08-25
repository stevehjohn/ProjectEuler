using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0087 : Puzzle
{
    private const int Max = 50_000_000;

    public override string GetAnswer()
    {
        var squares = GetRoots(2);

        var cubes = GetRoots(3);
        
        var quads = GetRoots(4);

        var sums = new HashSet<int>();
        
        for (var s = 0; s < squares.Count; s++)
        {
            for (var c = 0; c < cubes.Count; c++)
            {
                for (var q = 0; q < quads.Count; q++)
                {
                    var sum = squares[s] + cubes[c] + quads[q];
                    
                    if (sum < Max)
                    {
                        sums.Add(sum);
                    }
                }
            }
        }
    
        return sums.Count.ToString("N0");
    }

    private static List<int> GetRoots(int n)
    {
        var roots = new List<int>();

        var i = 2;

        while (true)
        {
            var power = (int) Math.Pow(i, n);

            if (power > Max)
            {
                break;
            }

            if (Maths.IsPrime(i))
            {
                roots.Add(power);
            }

            i++;
        }

        return roots;
    }
}