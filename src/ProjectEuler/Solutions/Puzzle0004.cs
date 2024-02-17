using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0004 : Puzzle
{
    public override string GetAnswer()
    {
        var palindromes = new List<int>();
        
        for (var left = 999; left > 900; left--)
        {
            for (var right = 999; right > 900; right--)
            {
                var product = left * right;

                if (product.IsPalindrome())
                {
                    palindromes.Add(product);
                }
            }
        }

        return palindromes.Max().ToString("N0");
    }
}