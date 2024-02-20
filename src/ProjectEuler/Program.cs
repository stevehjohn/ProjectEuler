using System.Diagnostics;
using System.Reflection;
using ProjectEuler.Infrastructure;

namespace ProjectEuler;

// ReSharper disable once InconsistentNaming
public static class Program
{
    private static string[] _answers;
    
    public static void Main(string[] args)
    {
        var puzzles = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Puzzle)) && ! t.IsAbstract)
            .OrderBy(t => t.Name);

        Console.WriteLine();

        var elapsed = 0d;

        _answers = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/Answers.txt");

        var count = 0;
        
        foreach (var puzzle in puzzles)
        {
            if (args.Length > 0 && ! puzzle.Name.EndsWith(args[0]))
            {
                continue;
            }

            elapsed += ExecutePuzzle(puzzle, args.Length == 0);

            count++;
        }
        
        Console.WriteLine($"{new string(' ', 30)}--------------");
        
        Console.WriteLine($"  {count,4} puzzle(s) solved in {$"{elapsed / 1_000:N0}",15}ms");

        Console.WriteLine();
    }

    private static double ExecutePuzzle(Type puzzle, bool warmUp)
    {
        var instance = Activator.CreateInstance(puzzle) as Puzzle;

        if (instance == null)
        {
            return 0;
        }

        if (warmUp)
        {
            instance.GetAnswer();
        }

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