using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0004 : Puzzle
{
    public override string GetAnswer()
    {
        var palindromes = new List<int>();
        
        for (var left = 999; left > 100; left--)
        {
            for (var right = 999; right > 100; right--)
            {
                var product = left * right;

                if (IsPalindrome(product))
                {
                    palindromes.Add(product);
                }
            }
        }

        return palindromes.Max().ToString("N0");
    }

    private static bool IsPalindrome(int number)
    {
        var text = number.ToString();

        for (var i = 0; i < text.Length / 2; i++)
        {
            if (text[i] != text[^(i + 1)])
            {
                return false;
            }
        }

        return true;
    }
}