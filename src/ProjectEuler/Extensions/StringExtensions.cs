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

    public static int CountCharacters(this string text, char character)
    {
        var count = 0;
        
        for (var i = 0; i < text.Length; i++)
        {
            if (text[i] == character)
            {
                count++;
            }
        }

        return count;
    }

    public static int CountOccurrences(this string text, string subString)
    {
        var count = 0;
        
        var i = 0;
        
        while (i < text.Length && (i = text.IndexOf(subString, i, StringComparison.InvariantCulture) + 1) != 0)
        {
            i++;
            
            count++;
        }
        
        return count;
    }
}