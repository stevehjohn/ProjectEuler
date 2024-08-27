using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0093 : Puzzle
{
    private static readonly int[] Digits = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    private static readonly char[] Operators = ['+', '-', '/', '*'];

    private static readonly char[][] Arrangements =
    [
        ['o', 'x', 'o', 'x', 'o', 'x', 'x'],
        ['o', 'r', 'o', 'x', 'x', 'x', 'm', 'o', 'x', 'x']
    ];
    
    private readonly Stack<double> _stack = new();
    
    public override string GetAnswer()
    {
        var combinations = GenerateCombinations(Digits, 4).ToList();

        var operators = Operators.GetCombinationsWithRepetition(3).ToList();

        int[] answer = [];

        var max = 0;
        
        foreach (var combination in combinations)
        {
            var results = new HashSet<int>();

            foreach (var operatorCombination in operators)
            {
                var permutations = combination.GetPermutations();

                foreach (var permutation in permutations)
                {
                    var result = Evaluate(permutation, operatorCombination);

                    results.Add(result);
                }
            }

            var length = GetRunLength(results);

            if (length > max)
            {
                max = length;

                answer = combination;
                
                Console.WriteLine($"{length}:");
                
                Console.WriteLine(string.Join(' ', results));
                
                Console.WriteLine();
            }
        }

        return string.Join(string.Empty, answer);
    }

    private static int GetRunLength(HashSet<int> results)
    {
        var i = 0;
        
        while (results.Contains(i + 1))
        {
            i++;
        }

        return i - 1;
    }

    private int Evaluate(int[] permutation, char[] operatorPermutation)
    {
        _stack.Clear();
        
        for (var i = 0; i < 3; i++)
        {
            _stack.Push(operatorPermutation[i]);

            _stack.Push(permutation[i]);
        }
        
        _stack.Push(permutation[3]);

        while (_stack.Count > 1)
        {
            var left = _stack.Pop();

            var right = _stack.Pop();

            var operation = _stack.Pop();

            switch (operation)
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