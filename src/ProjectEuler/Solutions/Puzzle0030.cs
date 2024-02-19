using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0030 : Puzzle
{
    public override string GetAnswer()
    {
        var sums = new List<long>();
        
        for (var i = 1; i < 999_999; i++)
        {
            var digits = i.ToString();

            var sum = 0L;
            
            foreach (var digit in digits)
            {
                sum += (long) Math.Pow(digit - '0', 5);
            }

            if (sum == i)
            {
                sums.Add(i);
            }
        }

        return sums.Sum().ToString("N0");
    }
}