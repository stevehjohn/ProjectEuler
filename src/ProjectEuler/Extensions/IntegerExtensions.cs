namespace ProjectEuler.Extensions;

public static class IntegerExtensions
{
    public static bool IsPalindrome(this int number)
    {
        var text = number.ToString();

        for (var i = 0; i < text.Length / 2; i++)
        {
            if (text[i] != text[^(i + 1)])
            {
                return false;
            }
        }

        return true;
    }
}