namespace ProjectEuler.Extensions;

public static class IntegerExtensions
{
    public static bool IsPalindrome(this int number)
    {
        var text = number.ToString();

        return text.IsPalindrome();
    }
}