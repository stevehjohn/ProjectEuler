using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0018 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var queue = new Queue<(int Sum, int Index)>();

        queue.Enqueue((ParseRow(0).Single(), 0));

        var rowNumber = 1;
        
        var row = ParseRow(rowNumber);
        
        while (queue.TryDequeue(out var item))
        {
            queue.Enqueue((item.Sum + row[item.Index], item.Index));
            
            queue.Enqueue((item.Sum + row[item.Index + 1], item.Index + 1));
            
            if (item.Index + 2 == row.Count)
            {
                rowNumber++;

                if (rowNumber == Input.Length)
                {
                    break;
                }

                row = ParseRow(rowNumber);
            }
        }

        return queue.MaxBy(i => i.Sum).Sum.ToString("N0");
    }
    
    private List<int> ParseRow(int rowNumber)
    {
        return Input[rowNumber].Split(' ').Select(int.Parse).ToList();
    }
}