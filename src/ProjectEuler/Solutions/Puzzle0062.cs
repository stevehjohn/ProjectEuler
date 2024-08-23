using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0062 : Puzzle
{
    public override string GetAnswer()
    {
        var number = 0L;

        var numbers = new List<string>();

        while (true)
        {
            var text = new string($"{number * number * number}".ToCharArray().Order().ToArray());

            numbers.Add(text);

            if (numbers.Count(n => n == text) == 5)
            {
                var index = (long) numbers.IndexOf(text);

                return (index * index * index).ToString("N0");
            }

            number++;
        }
    }
}