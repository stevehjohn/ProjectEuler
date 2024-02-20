#define DUMP
#if DUMP
using System.Diagnostics;
#endif
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

#if DUMP
            Console.CursorVisible = false;
            
            Dump(sudoku);
            
            Console.WriteLine();
            
            var sw = Stopwatch.StartNew();
#endif
            
            var solution = Solve(sudoku);

#if DUMP            
            sw.Stop();

            Console.Clear();
            
            Console.WriteLine($"\nPuzzle {i + 1} solution found in {sw.Elapsed.TotalMilliseconds}ms\n");
            
            Dump(sudoku, solution);
            
            Console.WriteLine("\nSolving next puzzle.\n");

            Console.CursorVisible = true;
#endif
            
            sum += solution[0, 0] * 100 + solution[1, 0] * 10 + solution[2, 0];
        }

        return sum.ToString("N0");
    }

#if DUMP    
    private static void Dump(int[,] sudoku, int[,] solution = null)
    {
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] == 0)
                {
                    Console.Write("  ");
                    
                    continue;
                }

                Console.Write($"{sudoku[x, y]} ");
            }
            
            Console.Write("  ");

            if (solution == null)
            {
                Console.WriteLine();
                
                continue;
            }

            for (var x = 0; x < 9; x++)
            {
                Console.Write($"{solution[x, y]} ");
            }
            
            Console.WriteLine();
        }
    }
#endif

    private static int[,] Solve(int[,] sudoku)
    {
        var queue = new Queue<int[,]>();
        
        queue.Enqueue(sudoku);

#if DUMP
        var cY = Console.CursorTop;
#endif
        
        while (queue.TryDequeue(out var puzzle))
        {
#if DUMP
            Console.CursorTop = cY;

            Dump(sudoku, puzzle);
            
            Console.WriteLine($"Queue: {queue.Count}      \n");
#endif
            
            var solutions = SolveStep(puzzle);

            foreach (var solution in solutions)
            {
                if (IsSolved(solution))
                {
                    return solution;
                }

                queue.Enqueue(solution);
            }
        }

        return null;
    }

    private static bool IsSolved(int[,] sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            var unique = new HashSet<int>();
            
            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] == 0)
                {
                    return false;
                }

                unique.Add(sudoku[x, y]);
            }

            if (unique.Count < 9)
            {
                return false;
            }
        }

        for (var x = 0; x < 9; x++)
        {
            var unique = new HashSet<int>();
            
            for (var y = 0; y < 9; y++)
            {
                if (sudoku[x, y] == 0)
                {
                    return false;
                }

                unique.Add(sudoku[x, y]);
            }

            if (unique.Count < 9)
            {
                return false;
            }
        }

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                var unique = new HashSet<int>();

                for (var x1 = 0; x1 < 3; x1++)
                {
                    for (var y1 = 0; y1 < 3; y1++)
                    {
                        unique.Add(sudoku[x * 3 + x1, y * 3 + y1]);
                    }
                }

                if (unique.Count < 9)
                {
                    return false;
                }
            }
            
        }

        return true;
    }
    
    private static bool IsValid(int[,] sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            var unique = new HashSet<int>();

            var count = 0;
            
            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] != 0)
                {
                    count++;

                    unique.Add(sudoku[x, y]);
                }
            }

            if (unique.Count < count)
            {
                return false;
            }
        }

        for (var x = 0; x < 9; x++)
        {
            var unique = new HashSet<int>();

            var count = 0;
            
            for (var y = 0; y < 9; y++)
            {
                if (sudoku[x, y] != 0)
                {
                    count++;

                    unique.Add(sudoku[x, y]);
                }
            }

            if (unique.Count < count)
            {
                return false;
            }
        }

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                var unique = new HashSet<int>();

                var count = 0;
                
                for (var x1 = 0; x1 < 3; x1++)
                {
                    for (var y1 = 0; y1 < 3; y1++)
                    {
                        if (sudoku[x * 3 + x1, y * 3 + y1] != 0)
                        {
                            count++;
                            
                            unique.Add(sudoku[x * 3 + x1, y * 3 + y1]);
                        }
                    }
                }

                if (unique.Count < count)
                {
                    return false;
                }
            }
            
        }

        return true;
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

            if (IsValid(copy))
            {
                solutions.Add(copy);
            }
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