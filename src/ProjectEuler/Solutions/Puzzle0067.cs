using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0067 : Puzzle
{
    private readonly Dictionary<int, List<int>> _rowCache = new();
    
    public override string GetAnswer()
    {
        LoadInput();

        var queue = new Queue<(int Sum, int Index, int Row)>();

        queue.Enqueue((GetRow(0).Single(), 0, 1));
        
        var max = 0;

        var results = new List<int>();
        
        while (queue.TryDequeue(out var item))
        {
            max = Math.Max(max, item.Sum);
            
            if (item.Sum < max - 99)
            {
                continue;
            }

            if (item.Row == Input.Length)
            {
                results.Add(item.Sum);
                
                continue;
            }

            var row = GetRow(item.Row);
            
            queue.Enqueue((item.Sum + row[item.Index], item.Index, item.Row + 1));
            
            queue.Enqueue((item.Sum + row[item.Index + 1], item.Index + 1, item.Row + 1));
        }

        return results.Max().ToString("N0");
    }
    
    private List<int> GetRow(int rowNumber)
    {
        if (! _rowCache.TryGetValue(rowNumber, out var row))
        {
            row = Input[rowNumber].Split(' ').Select(int.Parse).ToList();
            
            _rowCache.Add(rowNumber, row);
        }

        return row;
    }
}