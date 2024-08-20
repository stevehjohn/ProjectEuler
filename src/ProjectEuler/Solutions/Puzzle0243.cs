using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0243 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(100);

        var r = 1d;

        var queue = new PriorityQueue<(double, int, double), double>();
        
        queue.Enqueue((2, 1, 1), 2);

        var n = 0d;
        
        while (queue.Count > 0 && r >= 15499d / 94744d)
        {
            (n, var i, var phi) = queue.Dequeue();

            r = phi / (n - 1);

            var p = primes[i - 1];

            var q = primes[i];
            
            queue.Enqueue((p * n, i, p * phi), p * n);
            
            queue.Enqueue((q * n, i + 1, (q - 1) * phi), q * n);
        }

        return n.ToString("N0");
    }
}