using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0121 : Puzzle
{
    private const int MaxRounds = 4;
    
    public override string GetAnswer()
    {
        var wins = GenerateWins();

        foreach (var win in wins)
        {
            Console.WriteLine($"{win:b8}");
        }
        
        return "0";
    }

    private List<uint> GenerateWins()
    {
        var i = 1;

        var target = MaxRounds / 2 + 1;

        var wins = new List<uint>();

        var grayCode = 0u;
        
        while ((grayCode & (1 << MaxRounds)) == 0)
        {
            grayCode = (uint) (i ^ (i >> 1)); 
            
            if (BitOperations.PopCount(grayCode) >= target)
            {
                wins.Add(grayCode);
            }

            i++;
        }

        return wins;
    }
}