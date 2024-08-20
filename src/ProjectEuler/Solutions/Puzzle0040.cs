using System.Text;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0040 : Puzzle
{
    private const int Digits = 1_000_000;
    
    public override string GetAnswer()
    {
        var number = new StringBuilder(Digits * 2);

        number.Append("0.");
        
        var n = 1;

        var target = 1;

        var product = 1;
        
        while (target <= Digits)
        {
            number.Append(n);

            n++;

            if (number.Length > target + 1)
            {
                product *= number[target + 1] - '0';
                
                target *= 10;
            }
        }

        return product.ToString("N0");
    }
}