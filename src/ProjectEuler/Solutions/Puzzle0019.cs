using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0019 : Puzzle
{
    public override string GetAnswer()
    {
        var date = new DateTime(1901, 1, 1);

        var count = 0;

        while (date < new DateTime(2000, 12, 31))
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                count++;
            }

            date = date.AddMonths(1);
        }

        return count.ToString("N0");
    }
}