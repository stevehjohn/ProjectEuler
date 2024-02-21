//#define DUMP
#if DUMP
using System.Diagnostics;
#endif
using System.Numerics;
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
        
        for (var i = 0; i < Input.Length / 10; i++)
        {
            var sudoku = LoadSudoku(i);

#if DUMP
            Console.CursorVisible = false;
            
            var sw = Stopwatch.StartNew();
#endif
            
            var solution = Solve(sudoku);

#if DUMP            
            sw.Stop();

            Console.Clear();
            
            Console.WriteLine($"\nPuzzle {i + 1} solution found in {sw.Elapsed.TotalMilliseconds:N3}ms\n");
            
            Dump(sudoku, solution);

            if (i < 49)
            {
                Console.WriteLine("\nSolving next puzzle.\n");
            }
            else
            {
                Console.WriteLine();
            }

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
                if (solution[x, y] == 0)
                {
                    Console.Write("  ");
                    
                    continue;
                }
                
                Console.Write($"{solution[x, y]} ");
            }
            
            Console.WriteLine();
        }
    }
#endif

    private static int[,] Solve(int[,] sudoku)
    {
        var queue = new PriorityQueue<int[,], int>();
        
        queue.Enqueue(sudoku, 0);

#if DUMP
        var cY = Console.CursorTop;
#endif
        
        while (queue.TryDequeue(out var puzzle, out _))
        {
#if DUMP
            Console.CursorTop = cY;

            Dump(sudoku, puzzle);
            
            Console.WriteLine($"\nQueue: {queue.Count}      \n");
#endif
            
            var solutions = SolveStep(puzzle);

            foreach (var solution in solutions)
            {
                if (solution.Score == 0)
                {
                    return solution.Sudoku;
                }
                
                queue.Enqueue(solution.Sudoku, solution.Score);
            }
        }

        return null;
    }
    
    private static bool IsValid(int[,] sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            var rowSet = 0u;

            var columnSet = 0u;
            
            var countRow = 0;

            var countColumn = 0;
            
            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] != 0)
                {
                    countRow++;

                    rowSet |= (uint) 1 << sudoku[x, y];
                }

                if (sudoku[y, x] != 0)
                {
                    countColumn++;

                    columnSet |= (uint) 1 << sudoku[y, x];
                }
            }

            if (BitOperations.PopCount(rowSet) < countRow || BitOperations.PopCount(columnSet) < countColumn)
            {
                return false;
            }
        }

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                var set = 0u;
                
                var count = 0;
                
                for (var x1 = 0; x1 < 3; x1++)
                {
                    for (var y1 = 0; y1 < 3; y1++)
                    {
                        if (sudoku[x * 3 + x1, y * 3 + y1] != 0)
                        {
                            count++;

                            set |= (uint) 1 << sudoku[x * 3 + x1, y * 3 + y1];
                        }
                    }
                }

                if (BitOperations.PopCount(set) < count)
                {
                    return false;
                }
            }
            
        }

        return true;
    }

    private static List<(int[,] Sudoku, int Score)> SolveStep(int[,] sudoku)
    {
        var rowCandidates = new Dictionary<int, HashSet<int>>();
        
        var columnCandidates = new Dictionary<int, HashSet<int>>();
        
        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            columnCandidates[y] = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            for (var x = 0; x < 9; x++)
            {
                var value = sudoku[x, y];

                if (value > 0)
                {
                    rowCandidates[y].Remove(sudoku[x, y]);
                }
            }
            
            for (var x = 0; x < 9; x++)
            {
                var value = sudoku[y, x];

                if (value > 0)
                {
                    columnCandidates[y].Remove(sudoku[y, x]);
                }
            }
        }

        var position = (X: -1, Y: -1);

        var values = new List<int>();
        
        for (var y = 0; y < 9; y++)
        {
            var row = rowCandidates[y];

            if (row.Count == 0)
            {
                continue;
            }

            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x, y] != 0)
                {
                    continue;
                }

                var column = columnCandidates[x];

                if (column.Count == 0)
                {
                    continue;
                }

                var common = row.Intersect(column).ToList();

                if (values.Count == 0 || common.Count < values.Count)
                {
                    position = (x, y);

                    values = common;

                    if (values.Count == 1)
                    {
                        goto next;
                    }
                }
            }
        }
        next:

        var solutions = new List<(int[,] Sudokus, int Score)>();

        foreach (var move in values)
        {
            var copy = new int[9, 9];

            var score = 80;
            
            for (var y = 0; y < 9; y++)
            {
                for (var x = 0; x < 9; x++)
                {
                    var value = sudoku[x, y];

                    copy[x, y] = value;

                    if (value != 0)
                    {
                        score--;
                    }
                }
            }

            copy[position.X, position.Y] = move;

            if (IsValid(copy))
            {
                solutions.Add((copy, score));
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