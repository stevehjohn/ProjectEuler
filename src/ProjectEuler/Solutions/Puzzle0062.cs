using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0062 : Puzzle
{
    public override string GetAnswer()
    {
        var number = 0L;

        var characters = string.Empty;

        while (true)
        {
            var text = new string((number * number * number).ToString().ToCharArray().Order().ToArray());

            characters = $"{characters}{text}";

            if (characters.CountOccurrences(text) == 5)
            {
                var index = (long) characters.IndexOf(text, StringComparison.InvariantCulture) + 1;

                return (index * index * index).ToString("N0");
            }

            number++;
        }
    }
}