using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0049 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(10_000);

        return "Unknown";
    }
}