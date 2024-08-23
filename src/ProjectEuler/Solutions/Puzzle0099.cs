using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0099 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var line = 0;

        var max = 0d;
        
        for (var i = 0; i < Input.Length; i++)
        {
            var parts = Input[i].Split(',');

            var value = int.Parse(parts[1]) * Math.Log(int.Parse(parts[0]));

            if (value > max)
            {
                max = value;

                line = i;
            }
        }

        return (line + 1).ToString("N0");
    }
}