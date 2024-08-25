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

    private readonly Stack<double> _stack = new();
    
    public override string GetAnswer()
    {
        var combinations = GenerateCombinations(Digits, 4).ToList();

        combinations = [[1, 2, 3, 4]];
        
        var operators = GenerateCombinations(Operators, 3).ToList();

        var results = new HashSet<int>();
        
        foreach (var combination in combinations)
        {
            foreach (var operatorCombination in operators)
            {
                var permutations = combination.GetPermutations();

                foreach (var permutation in permutations)
                {
                    var operatorPermutations = operatorCombination.GetPermutations();

                    foreach (var operatorPermutation in operatorPermutations)
                    {
                        var result = Evaluate(permutation, operatorPermutation);

                        results.Add(result);
                    }
                }
            }
        }
        
        Console.WriteLine(string.Join(' ', results.Order()));

        return "WIP";
    }

    private int Evaluate(int[] permutation, char[] operatorPermutation)
    {
        _stack.Clear();
        
        _stack.Push(permutation[0]);

        for (var i = 0; i < 3; i++)
        {
            _stack.Push(permutation[i + 1]);
            
            _stack.Push(operatorPermutation[i]);
        }

        while (_stack.Count > 1)
        {
            var left = _stack.Pop();

            var right = _stack.Pop();

            switch (_stack.Pop())
            {
                case '+':
                    _stack.Push(left + right);
                    
                    break;
                
                case '-':
                    _stack.Push(left - right);
                    
                    break;
                
                case '*':
                    _stack.Push(left * right);
                    
                    break;
                
                default:
                    _stack.Push(left / right);
                    
                    break;
            }
        }

        var result = _stack.Pop();

        if (result < 1 || result % 1 != 0)
        {
            return 0;
        }

        return (int) result;
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