using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0041 : Puzzle
{
    public override string GetAnswer()
    {
        var digits = new[] { '1', '2', '3', '4', '5', '6', '7' };

        var permutations = digits.GetPermutations();

        var max = 0L;

        foreach (var permutation in permutations)
        {
            var number = long.Parse(new string(permutation));

            if (number > max && Maths.IsPrime(number))
            {
                max = number;
            }
        }

        return max.ToString("N0");
    }
}