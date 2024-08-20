using System.Text;

namespace ProjectEuler.Extensions;

public static class StringExtensions
{
    public static bool IsPalindrome(this string text)
    {
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