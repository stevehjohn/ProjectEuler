using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0051 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000);
        
        throw new NotImplementedException();
    }
}