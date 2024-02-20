using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0083 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var matrix = ParseInput();

        var queue = new PriorityQueue<(int X, int Y, int Sum), int>();

        queue.Enqueue((0, 0, matrix[0, 0]), matrix[0, 0]);

        var visited = new HashSet<(int, int)> { (0, 0) };

        while (queue.TryDequeue(out var item, out _))
        {
            if (item.X == 79 && item.Y == 79)
            {
                return item.Sum.ToString("N0");
            }

            if (item.X > 0 && visited.Add((item.X - 1, item.Y)))
            {
                queue.Enqueue((item.X - 1, item.Y, item.Sum + matrix[item.X - 1, item.Y]), item.Sum + matrix[item.X - 1, item.Y]);
            }

            if (item.X < 79 && visited.Add((item.X + 1, item.Y)))
            {
                queue.Enqueue((item.X + 1, item.Y, item.Sum + matrix[item.X + 1, item.Y]), item.Sum + matrix[item.X + 1, item.Y]);
            }

            if (item.Y > 0 && visited.Add((item.X, item.Y - 1)))
            {
                queue.Enqueue((item.X, item.Y - 1, item.Sum + matrix[item.X, item.Y - 1]), item.Sum + matrix[item.X, item.Y - 1]);
            }

            if (item.Y < 79 && visited.Add((item.X, item.Y + 1)))
            {
                queue.Enqueue((item.X, item.Y + 1, item.Sum + matrix[item.X, item.Y + 1]), item.Sum + matrix[item.X, item.Y + 1]);
            }
        }
        return "Unknown";
    }

    private int[,] ParseInput()
    {
        var matrix = new int[80, 80];

        var i = 0;
        
        foreach (var line in Input)
        {
            var parts = line.Split(',');

            for (var x = 0; x < 80; x++)
            {
                matrix[x, i] = int.Parse(parts[x]);
            }

            i++;
        }
        
        return matrix;
    }
}