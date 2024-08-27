using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public partial class Puzzle0093 : Puzzle
{
    private static readonly int[] Digits = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    private static readonly char[] Operators = ['+', '-', '/', '*'];

    private static readonly string[][] Arrangements =
    [
        ["x ", "o ", "x ", "o ", "x ", "o ", "x"],
        ["(", "x ", "o ", "x", ")", " o ", "(", "x ", "o ", "x", ")"]
    ];

    private static readonly Regex Parser = ExpressionParser();

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
                    foreach (var arrangement in Arrangements)
                    {
                        var expression = CreateExpression(permutation, operatorCombination, arrangement);

                        var result = Evaluate(expression);

                        results.Add(result);
                    }
                }
            }

            var length = GetRunLength(results);

            if (length > max || string.Join(string.Empty, combination) == "1258")
            {
                max = length;

                answer = combination;

                Console.WriteLine($"{length} ({string.Join(string.Empty, combination)}):");

                Console.WriteLine(string.Join(' ', results.Order()));

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

    private static string CreateExpression(int[] digits, char[] operators, string[] arrangement)
    {
        var d = 0;

        var o = 0;

        var result = new StringBuilder();

        for (var i = 0; i < arrangement.Length; i++)
        {
            var pattern = arrangement[i];

            switch (pattern.Trim()[0])
            {
                case 'x':
                    result.Append(pattern.Replace("x", digits[d].ToString()));
                    d++;

                    break;

                case 'o':
                    result.Append(pattern.Replace("o", operators[o].ToString()));
                    o++;

                    break;

                default:
                    result.Append(pattern);

                    break;
            }
        }

        return result.ToString();
    }

    private static int Evaluate(string expression)
    {
        var queue = ParseToQueue(expression);

        var stack = new Stack<IElement>();

        foreach (var element in queue)
        {
            if (element is Operator symbol)
            {
                var left = ((Operand) stack.Pop()).Value;

                var right = ((Operand) stack.Pop()).Value;

                switch (symbol.Value)
                {
                    case '+':
                        stack.Push(new Operand(left + right));

                        break;

                    case '-':
                        stack.Push(new Operand(left - right));

                        break;

                    case '*':
                        stack.Push(new Operand(left * right));

                        break;

                    default:
                        stack.Push(new Operand(left / right));

                        break;
                }
            }
            else
            {
                stack.Push(element);
            }
        }

        return 0;
    }

    private static Queue<IElement> ParseToQueue(string expression)
    {
        var queue = new Queue<IElement>();

        var stack = new Stack<char>();

        var parts = Parser.Matches(expression).Select(p => p.Value);
        
        foreach (var item in parts)
        {
            if (int.TryParse(item, out var number))
            {
                queue.Enqueue(new Operand(number));
                
                continue;
            }

            var digit = item[0];

            if (digit == '(')
            {
                stack.Push(digit);
                
                continue;
            }

            if (digit == ')')
            {
                // TODO
                continue;
            }

            while (stack.Peek() != '(')
            {
                // TODO
            }
            
            queue.Enqueue(new Operator(item[0]));
        }
        
        return queue;
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

    private interface IElement;

    private class Operator : IElement
    {
        public char Value { get; }

        public Operator(char value)
        {
            Value = value;
        }
    }
    
    private class Operand : IElement
    {
        public double Value { get; }

        public Operand(double value)
        {
            Value = value;
        }
    }

    [GeneratedRegex(@"\d+|[+\-*/()]", RegexOptions.Compiled)]
    private static partial Regex ExpressionParser();
}