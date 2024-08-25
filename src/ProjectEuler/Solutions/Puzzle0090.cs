using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0090 : Puzzle
{
    private static readonly int[] Primes = [1, 4, 9, 16, 25, 36, 49, 64, 81];

    private static readonly int[] Digits = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    
    public override string GetAnswer()
    {
        var combinations = GenerateDigitCombinations().ToList();
        
        foreach (var combination in combinations)
        {
            Console.WriteLine(string.Join(", ", combination));
        }

        for (var l = 0; l < combinations.Count; l++)
        {
            for (var r = l + 1; r < combinations.Count; r++)
            {
                if (CanDisplayPrimes(combinations[l], combinations[r]))
                {
                    Console.WriteLine($"{string.Join(", ", combinations[l])}  {string.Join(", ", combinations[r])}");
                }
            }
        }

        return "TODO";
    }

    private static bool CanDisplayPrimes(int[] left, int[] right)
    {
        return false;
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