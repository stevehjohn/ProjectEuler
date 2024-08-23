using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0206 : Puzzle
{
    private const long Maximum = 19_293_949_596_979_899;
    
    public override string GetAnswer()
    {
        var number = (long) Math.Ceiling(Math.Sqrt(Maximum));

        while (true)
        {
            number -= 2;

            var n = (number * number).ToString();

            var text = $"{n[0]}{n[2]}{n[4]}{n[6]}{n[8]}{n[10]}{n[12]}{n[14]}{n[16]}";

            if (text == "123456789")
            {
                return (number * 10).ToString("N0");
            }
        }
    }
}