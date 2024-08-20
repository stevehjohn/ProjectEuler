using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

public class Puzzle0089 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var saved = 0;
        
        foreach (var line in Input)
        {
            var copy = new string(line);

            while (true)
            {
                var temp = new string(copy);
                
                copy = copy.Replace("DCCCC", "CM");
                copy = copy.Replace("LXXXX", "XC");
                copy = copy.Replace("VIIII", "IX");
                copy = copy.Replace("CCCC", "CD");
                copy = copy.Replace("XXXX", "XL");
                copy = copy.Replace("IIII", "IV");
                
                if (copy == temp)
                {
                    break;
                }
            }

            saved += line.Length - copy.Length;
        }

        return saved.ToString("N0");
    }
}