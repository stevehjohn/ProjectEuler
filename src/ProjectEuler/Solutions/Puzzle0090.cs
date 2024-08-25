using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0090 : Puzzle
{
    private static readonly (int Bit, int Left, int Right)[] Primes =
    [
        (0b0000_0000_0001, 0, 1),
        (0b0000_0000_0010, 0, 4),
        (0b0000_0000_0100, 0, 9),
        (0b0000_0000_0100, 0, 6),
        (0b0000_0000_1000, 1, 6),
        (0b0000_0000_1000, 1, 9),
        (0b0000_0001_0000, 2, 5),
        (0b0000_0010_0000, 3, 6),
        (0b0000_0010_0000, 3, 9),
        (0b0000_0100_0000, 4, 9),
        (0b0000_0100_0000, 4, 6),
        (0b0000_1000_0000, 6, 4),
        (0b0000_1000_0000, 9, 4),
        (0b0001_0000_0000, 8, 1)
    ];

    private static readonly int[] Digits = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    
    public override string GetAnswer()
    {
        var combinations = GenerateDigitCombinations().ToList();

        var count = 0;
        
        for (var l = 0; l < combinations.Count; l++)
        {
            for (var r = l + 1; r < combinations.Count; r++)
            {
                var left = combinations[l];

                var right = combinations[r];
                
                if (CanDisplayPrimes(left, right))
                {
                    count++;
                }
            }
        }

        return count.ToString("N0");
    }

    private static bool CanDisplayPrimes(int[] left, int[] right)
    {
        var found = 0;
        
        for (var i = 0; i < Primes.Length; i += 2)
        {
            if (! (left.Contains(Primes[i].Left) && right.Contains(Primes[i + 1].Right))
                && ! (right.Contains(Primes[i].Left) && left.Contains(Primes[i + 1].Right)))
            {
                found |= Primes[i].Bit;
            }
        }

        return found == 0b0001_1111_1111;
    }

    private static IEnumerable<int[]> GenerateDigitCombinations()
    {
        var i = 1;

        var grayCode = 0u;
        
        while ((grayCode & (1ul << 10)) == 0)
        {
            grayCode = (uint) (i ^ (i >> 1)); 
            
            if (BitOperations.PopCount(grayCode) == 6)
            {
                var digits = new int[6];

                var used = 0;

                var position = 0;

                var bit = 1;
                
                while (used < 6)
                {
                    if ((grayCode & bit) > 0)
                    {
                        digits[used] = Digits[position];

                        used++;
                    }

                    position++;

                    bit <<= 1;
                }
                
                yield return digits;
            }

            i++;
        }
    }
}