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
        
        primes.Add(2);

        for (var i = 3; i < max; i += 2)
        {
            primes.Add(i);
        }

        var index = 0;

        while (index < primes.Count)
        {
            Console.WriteLine($"{primes[index]}, {primes.Count}");
            
            var value = primes[index];
            
            if (! IsPrime(value))
            {
                primes.RemoveAt(index);
                
                continue;
            }

            for (var i = index + 1; i < primes.Count; i++)
            {
                if (primes[i] % value == 0)
                {
                    primes.RemoveAt(i);
                }
            }

            index++;
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