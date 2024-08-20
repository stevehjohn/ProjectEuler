using System.Text;

namespace ProjectEuler.Extensions;

public static class IntegerExtensions
{
    private static readonly Dictionary<int, string> NumberWords = new()
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
        { 10, "ten" },
        { 11, "eleven" },
        { 12, "twelve" },
        { 13, "thirteen" },
        { 14, "fourteen" },
        { 15, "fifteen" },
        { 16, "sixteen" },
        { 17, "seventeen" },
        { 18, "eighteen" },
        { 19, "nineteen" },
        { 20, "twenty" },
        { 30, "thirty" },
        { 40, "forty" },
        { 50, "fifty" },
        { 60, "sixty" },
        { 70, "seventy" },
        { 80, "eighty" },
        { 90, "ninety" }
    };

    private static readonly Dictionary<int, (string Word, int DivisorToNext)> Boundaries = new()
    {   
        { 1_000, ("thousand", 10) },
        { 100, ("hundred", 100) },
        { 1, (string.Empty, 1) }
    };

    public static string NumberToEnglish(this int number)
    {
        var result = new StringBuilder();
        
        foreach (var boundary in Boundaries)
        {
            if (number >= boundary.Key)
            {
                result.Append($"{GetNumberWord(number / boundary.Key)} {boundary.Value.Word} ");

                number -= number / boundary.Key * boundary.Key;

                if (number < 100 && number > 0)
                {
                    result.Append("and ");
                }
            }
        }

        return result.ToString().Trim();
    }

    private static string GetNumberWord(int number)
    {
        if (NumberWords.TryGetValue(number, out var word))
        {
            return word;
        }

        return $"{NumberWords[number / 10 * 10]} {NumberWords[number % 10]} ";
    }
    
    public static bool IsPalindrome(this int number)
    {
        var text = number.ToString();

        return text.IsPalindrome();
    }
}