using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0035 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000);

        var hashes = primes.ToHashSet();

        var count = 0;
        
        foreach (var prime in primes)
        {
            var text = prime.ToString();

            var pass = true;
            
            for (var i = 0; i < text.Length - 1; i++)
            {
                text = $"{text[1..]}{text[0]}";

                if (! hashes.Contains(int.Parse(text)))
                {
                    pass = false;
                    
                    break;
                }
            }

            if (pass)
            {
                count++;
            }
        }

        return count.ToString("N0");
    }
}