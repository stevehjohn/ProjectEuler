using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0090 : Puzzle
{
    private static readonly int[] Primes = [0, 1, 0, 4, 0, 9, 1, 6, 2, 5, 3, 6, 4, 9, 6, 4, 8, 1];

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
                    
                    Console.WriteLine($"{string.Join(", ", left)}  {string.Join(", ", right)}");
                }
            }
        }

        return count.ToString("N0");
    }

    private static bool CanDisplayPrimes(int[] left, int[] right)
    {
        for (var i = 0; i < Primes.Length; i += 2)
        {
            if (! (left.Contains(Primes[i]) && right.Contains(Primes[i + 1]))
                && ! (right.Contains(Primes[i]) && left.Contains(Primes[i + 1])))
            {
                return false;
            }
        }

        return true;
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