using System.Text;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0024 : Puzzle
{
    private static readonly int[] Factorials = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880];
    
    public override string GetAnswer()
    {
        var digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var result = new StringBuilder();

        var target = 999_999;
        
        for (var i = digits.Count - 1; i >= 0; i--)
        {
            var index = target / Factorials[i];

            target %= Factorials[i];

            result.Append(digits[index]);
            
            digits.RemoveAt(index);
        }

        return long.Parse(result.ToString()).ToString("N0");
    }
}