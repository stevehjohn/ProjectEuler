using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0010 : Puzzle
{
    public override string GetAnswer()
    {
        return Maths.GetPrimes(2_000_000).Sum().ToString("N0");
    }
}