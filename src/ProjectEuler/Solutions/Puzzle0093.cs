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

    private static readonly char[][] Parentheses =
        [
            ['x', 'o', 'x', 'o', 'x', 'o', 'x'],
            ['(', 'x', 'o', 'x', ')', 'o', '(', 'x', 'o', 'x', ')']
        ];

    private readonly Stack<double> _stack = new();

    private readonly Stack<double> _memory = new();
    
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
                foreach (var parenthesis in Parentheses)
                {
                    var permutations = combination.GetPermutations();

                    foreach (var permutation in permutations)
                    {
                        var operations = FormExpression(parenthesis, operatorCombination, permutation);
                    
                        var result = Evaluate(permutation, operations);

                        results.Add(result);
                    }
                }
            }

            var length = GetRunLength(results);

            if (length > max)
            {
                max = length;

                answer = combination;
            }
        }

        return string.Join(string.Empty, answer);
    }

    private static char[] FormExpression(char[] parentheses, char[] operators, int[] digits)
    {
        var result = new char[parentheses.Length];

        var o = 0;

        var d = 0;
        
        for (var i = 0; i < parentheses.Length; i++)
        {
            switch (parentheses[i])
            {
                case 'o':
                    result[i] = operators[o];
                    o++;
                    
                    break;
                
                case 'x':
                    result[i] = (char) digits[d];
                    d++;
                    
                    break;
                
                default:
                    result[i] = parentheses[i];
                    
                    break;
            }
        }

        return result;
    }

    private static int GetRunLength(HashSet<int> results)
    {
        var i = 0;
        
        while (results.Contains(i + 1))
        {
            i++;
        }

        return i;
    }

    private int Evaluate(int[] permutation, char[] operations)
    {
        _stack.Clear();
        
        for (var i = 0; i < operations.Length; i++)
        {
            _stack.Push(operations[i]);

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

        if (result % 1 != 0)
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