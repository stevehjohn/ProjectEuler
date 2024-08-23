using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0078 : Puzzle
{
    public override string GetAnswer()
    {
        var partitions = new List<long> { 1 };

        var i = 0;

        while (partitions[i] != 0)
        {
            i++;

            var partition = 0L;
            
            foreach (var item in GetPentagonals(i))
            {
                partition += item.Sign * partitions[(int) (i - item.Number)];

                partition %= 1_000_000;
            }

            if (partition == 0)
            {
            }

            partitions.Add(partition);
        }

        return i.ToString("N0");
    }

    private static IEnumerable<(long Sign, long Number)> GetPentagonals(long number)
    {
        var a = 1;

        var b = 2;

        var delta = 4;

        var sign = 1;

        while (a <= number)
        {
            yield return (sign, a);

            a += delta;

            if (b <= number)
            {
                yield return (sign, b);

                b += delta + 1;
            }

            delta += 3;

            sign = -sign;
        }
    }
}