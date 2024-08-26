namespace ProjectEuler.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<T[]> GetCombinationsWithRepetition<T>(this T[] array, int length)
    {
        var radix = array.Length;

        var digits = new int[length];

        while (true)
        {
            var result = new T[length];

            int i;
            
            for (i = 0; i < length; i++)
            {
                result[i] = array[digits[i]];
            }

            yield return result;

            i = 0;

            while (i < length)
            {
                digits[i]++;

                if (digits[i] < radix)
                {
                    break;
                }

                digits[i] = 0;

                i++;
            }

            if (i >= length)
            {
                yield break;
            }

        }
    }

    public static IEnumerable<T[]> GetPermutations<T>(this T[] array)
    {
        return GetPermutationsInternal(array.Length, array);
    }

    private static IEnumerable<T[]> GetPermutationsInternal<T>(int remainingIterations, T[] array)
    {
        while (remainingIterations > 0)
        {
            if (remainingIterations == 1)
            {
                yield return (T[]) array.Clone();
            }

            for (var i = 0; i < remainingIterations - 1; i++)
            {
                var permutations = GetPermutationsInternal(remainingIterations - 1, array);

                foreach (var permutation in permutations)
                {
                    yield return permutation;
                }

                if (remainingIterations % 2 == 0)
                {
                    (array[i], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[i]);
                }
                else
                {
                    (array[0], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[0]);
                }
            }

            remainingIterations -= 1;
        }
    }
}