using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0034 : Puzzle
{
    public override string GetAnswer()
    {
        var factorials = new[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

        var sum = 0;

        for (var i = 10; i < 100_000; i++)
        {
            var str = i.ToString();

            var factorialSum = 0;
            
            foreach (var c in str)
            {
                factorialSum += factorials[c - '0'];
            }

            if (factorialSum == i)
            {
                sum += i;
            }
        }

        return sum.ToString("N0");
    }
}