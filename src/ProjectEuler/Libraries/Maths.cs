using System.Numerics;

namespace ProjectEuler.Libraries;

public static class Maths
{
    public static BigInteger Factorial(BigInteger number)
    {
        var result = new BigInteger(1);

        while (number > 1)
        {
            result *= number;

            number--;
        }

        return result;
    }

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
        if (number == 1)
        {
            return false;
        }

        if (number < 4)
        {
            return true;
        }

        if (number % 2 == 0)
        {
            return false;
        }

        if (number < 9)
        {
            return true;
        }

        if (number % 3 == 0)
        {
            return false;
        }

        var r = (int) Math.Sqrt(number);
        
        var f = 5;

        while (f <= r)
        {
            if (number % f == 0)
            {
                return false;
            }

            if (number % (f + 2) == 0)
            {
                return false;
            }

            f += 6;
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