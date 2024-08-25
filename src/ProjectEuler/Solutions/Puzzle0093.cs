using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0093 : Puzzle
{
    private static readonly int[] Digits = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    private static readonly char[] Operators = ['+', '-', '\\', '*'];

    public override string GetAnswer()
    {
        var combinations = GenerateCombinations(Digits, 4).ToList();

        var operators = GenerateCombinations(Operators, 3).ToList();

        foreach (var combination in combinations)
        {
            foreach (var operatorCombination in operators)
            {
                GetChainLength(combination, operatorCombination);
            }
        }
        
        throw new NotImplementedException();
    }

    private static void GetChainLength(int[] combination, char[] operatorCombination)
    {
        var permutations = combination.GetPermutations();

        var results = new HashSet<int>();
        
        foreach (var permutation in permutations)
        {
            var operatorPermutations = operatorCombination.GetPermutations();

            foreach (var operatorPermutation in operatorPermutations)
            {
                var result = Evaluate(permutation, operatorPermutation);
            }
        }
        
        throw new NotImplementedException();
    }

    private static object Evaluate(int[] permutation, char[] operatorPermutation)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<T[]> GenerateCombinations<T>(T[] source, int length)
    {
        var i = 1;

        var grayCode = 0u;
        
        while ((grayCode & (1ul << source.Length)) == 0)
        {
            grayCode = (uint) (i ^ (i >> 1)); 
            
            if (BitOperations.PopCount(grayCode) == length)
            {
                var digits = new T[length];

                var used = 0;

                var position = 0;

                var bit = 1;
                
                while (used < length)
                {
                    if ((grayCode & bit) > 0)
                    {
                        digits[used] = source[position];

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