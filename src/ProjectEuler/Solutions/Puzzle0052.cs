using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0052 : Puzzle
{
    public override string GetAnswer()
    {
        var result = 1;

        while (true)
        {
            if (HasSameDigits(result, result)
                && HasSameDigits(result, result * 2)
                && HasSameDigits(result, result * 3)
                && HasSameDigits(result, result * 4)
                && HasSameDigits(result, result * 5)
                && HasSameDigits(result, result * 6))
            {
                break;
            }

            result++;
        }

        return result.ToString("N0");
    }

    private static bool HasSameDigits(int left, int right)
    {
        var leftString = left.ToString();

        var rightString = right.ToString();

        if (leftString.Length != rightString.Length)
        {
            return false;
        }

        foreach (var letter in leftString)
        {
            if (! rightString.Contains(letter))
            {
                return false;
            }
        }
        
        return true;
    }
}