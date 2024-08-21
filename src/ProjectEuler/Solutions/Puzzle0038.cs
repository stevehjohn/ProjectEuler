using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0038 : Puzzle
{
    public override string GetAnswer()
    {
        for (var i = 9_487; i >= 9_234; i--)
        {
            var result = 100_002 * i;

            if (IsPandigital(result))
            {
                return result.ToString("N0");
            }
        }

        return "Unknown";
    }

    private static bool IsPandigital(long number)
    {
        var all = new string(number.ToString().ToCharArray().Order().ToArray());

        return all == "123456789";
    }
}