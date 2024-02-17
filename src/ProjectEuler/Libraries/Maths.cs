using System.Numerics;

namespace ProjectEuler.Libraries;

public static class Maths
{
    public static List<long> GetPrimes(long max)
    {
        var primes = new List<long>();

        if (max < 2)
        {
            return primes;
        }

        var sieveSize = (max - 1) / 2;

        var sieve = new bool[sieveSize];

        var crossLimit = (Math.Sqrt(max) - 1) / 2;

        for (var i = 1; i <= crossLimit; i++)
        {
            if (! sieve[i])
            {
                for (var j = 2 * i * (i + 1); j < sieveSize; j += 2 * i + 1)
                {
                    sieve[j] = true;
                }
            }
        }
        
        primes.Add(2);

        for (var i = 1; i < sieveSize; i++)
        {
            if (! sieve[i])
            {
                primes.Add(2 * i + 1);
            }
        }

        return primes;
    }

    public static bool IsPrime(long number)
    {
        if (number == 2)
        {
            return true;
        }

        for (var i = 3; i < number; i += 2)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
    
    public static long LowestCommonMultiple(List<long> input)
    {
        var queue = new Queue<long>(input.Count * 2);

        foreach (var item in input)
        {
            queue.Enqueue(item);
        }
        
        while (true)
        {
            long left;
            
            long right;
            
            if (queue.Count == 2)
            {
                left = queue.Dequeue();

                right = queue.Dequeue();

                return left * right / GreatestCommonFactor(left, right);
            }

            left = queue.Dequeue();

            right = queue.Dequeue();

            var lowestCommonMultiple = left * right / GreatestCommonFactor(left, right);

            queue.Enqueue(lowestCommonMultiple);
        }
    }

    private static long GreatestCommonFactor(long left, long right)
    {
        var gcdExponentOnTwo = BitOperations.TrailingZeroCount(left | right);

        left >>= gcdExponentOnTwo;
        
        right >>= gcdExponentOnTwo;

        while (left != right)
        {
            if (left < right)
            {
                right -= left;

                right >>= BitOperations.TrailingZeroCount(right);
            }
            else
            {
                left -= right;

                left >>= BitOperations.TrailingZeroCount(left);
            }
        }

        return left << gcdExponentOnTwo;
    }
}