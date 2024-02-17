using System.Diagnostics;
using System.Reflection;
using ProjectEuler.Infrastructure;

namespace ProjectEuler;

public static class Program
{
    private static string[] _answers;
    
    public static void Main()
    {
        var puzzles = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Puzzle)) && ! t.IsAbstract)
            .OrderBy(t => t.Name);

        Console.WriteLine();

        var elapsed = 0d;

        _answers = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/Answers.clear");
        
        foreach (var puzzle in puzzles)
        {
            elapsed += ExecutePuzzle(puzzle);
        }
        
        Console.WriteLine($"{new string(' ', 30)}--------------");
        
        Console.WriteLine($"{$"{elapsed / 1_000:N0}",42}ms");

        Console.WriteLine();
    }

    private static double ExecutePuzzle(Type puzzle)
    {
        var instance = Activator.CreateInstance(puzzle) as Puzzle;

        if (instance == null)
        {
            return 0;
        }

        instance.GetAnswer();
        
        var stopwatch = Stopwatch.StartNew();

        var answer = instance.GetAnswer();
        
        stopwatch.Stop();

        var number = int.Parse(puzzle.Name[6..]);

        var colour = Console.ForegroundColor;
        
        if (answer != _answers[number - 1])
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        Console.WriteLine($"  {number,4}: {answer,-20}  {$"{stopwatch.Elapsed.TotalMicroseconds:N0}",12}μs");

        Console.ForegroundColor = colour;

        return stopwatch.Elapsed.TotalMicroseconds;
    }
}