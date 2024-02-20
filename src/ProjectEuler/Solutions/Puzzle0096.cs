using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();
        
        var sum = 0;
        
        for (var i = 0; i < 50; i++)
        {
            var sudoku = LoadSudoku(i);

            Dump(sudoku);
            
            Solve(sudoku);

            Dump(sudoku);
                
            Console.WriteLine();

            Console.ReadKey();

            sum += sudoku[0, 0] * 100 + sudoku[1, 0] * 10 + sudoku[2, 0];
        }

        return sum.ToString("N0");
    }

    private static void Dump(int[,] sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                Console.Write($"{sudoku[x, y]} ");
            }
            
            Console.WriteLine();
        }
    }

    private static void Solve(int[,] sudoku)
    {
        var queue = new Queue<int[,]>();
        
        queue.Enqueue(sudoku);

        while (queue.TryDequeue(out var puzzle))
        {
            var solutions = SolveStep(puzzle);

            foreach (var solution in solutions)
            {
                queue.Enqueue(solution);
            }
        }
    }

    private static List<int[,]> SolveStep(int[,] sudoku)
    {
        var rowCandidates = new Dictionary<int, List<int>>();
        
        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = new List<int>();
            
            for (var c = 1; c < 10; c++)
            {
                var found = false;
                
                for (var x = 0; x < 9; x++)
                {
                    if (sudoku[x, y] == c)
                    {
                        found = true;
                        
                        break;
                    }
                }

                if (! found)
                {
                    rowCandidates[y].Add(c);
                }
            }
        }

        var columnCandidates = new Dictionary<int, List<int>>();
        
        for (var x = 0; x < 9; x++)
        {
            columnCandidates[x] = new List<int>();
            
            for (var c = 1; c < 10; c++)
            {
                var found = false;
                
                for (var y = 0; y < 9; y++)
                {
                    if (sudoku[x, y] == c)
                    {
                        found = true;
                        
                        break;
                    }
                }

                if (! found)
                {
                    columnCandidates[x].Add(c);
                }
            }
        }

        var position = (X: -1, Y: -1);

        var values = new List<int>();
        
        for (var y = 0; y < 9; y++)
        {
            var row = rowCandidates[y];

            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] != 0)
                {
                    continue;
                }

                var column = columnCandidates[x];

                var common = row.Intersect(column).ToList();

                if (values.Count == 0 || common.Count < values.Count)
                {
                    position.X = x;

                    position.Y = y;

                    values = common;
                }
            }
        }

        var solutions = new List<int[,]>();

        foreach (var move in values)
        {
            var copy = new int[9, 9];
            
            Array.Copy(sudoku, copy, 81);

            copy[position.X, position.Y] = move;
            
            solutions.Add(copy);
        }
        
        return solutions;
    }

    private int[,] LoadSudoku(int number)
    {
        var start = number * 10 + 1;

        var matrix = new int[9, 9];

        for (var y = 0; y < 9; y++)
        {
            var line = Input[start + y];

            for (var x = 0; x < 9; x++)
            {
                matrix[x, y] = line[x] - '0';
            }
        }

        return matrix;
    }
}