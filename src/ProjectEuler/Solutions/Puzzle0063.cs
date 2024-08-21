using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0063 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 0;
        
        for (var i = 1; i < 10; i++)
        {
            var power = 1;

            while (true)
            {
                if (power == ((int) Math.Pow(i, power)).ToString().Length)
                {
                    count++;
                }
                else
                {
                    break;
                }

                power++;
            }
        }

        return count.ToString("N0");
    }
}