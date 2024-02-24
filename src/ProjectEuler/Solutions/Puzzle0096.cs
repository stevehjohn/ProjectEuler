using System.Diagnostics;
using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    private readonly List<(int Id, double Elapsed, int Solved)> _history = new();

    private (double Total, double Minimum, double Maximum) _elapsed = (0, double.MaxValue, 0);

    private (long Total, int Minimum, int Maximum) _steps = (0, int.MaxValue, 0);

    private int _maxPuzzleNumber;

    private Stopwatch _stopwatch;

    private object _statsLock = new();
    
    public override string GetAnswer()
    {
        LoadInput();
        
        var sum = 0;

        var solved = 0;
        
        Console.Clear();

        Console.CursorVisible = false;
        
        _stopwatch = Stopwatch.StartNew();
        
        Parallel.For(0, Input.Length,
            () => 0,
            (i, _, subTotal) => {
                var sudoku = LoadSudoku(i);

                var stopwatch = Stopwatch.StartNew();
                
                var solution = Solve(i, sudoku);
                
                stopwatch.Stop();
                
                lock (_statsLock)
                {
                    _history.Add((i, stopwatch.Elapsed.TotalMicroseconds, solved));

                    _elapsed.Total += stopwatch.Elapsed.TotalMicroseconds;

                    _elapsed.Minimum = Math.Min(_elapsed.Minimum, stopwatch.Elapsed.TotalMicroseconds);

                    _elapsed.Maximum = Math.Max(_elapsed.Maximum, stopwatch.Elapsed.TotalMicroseconds);

                    solved++;

                    Dump(sudoku, solution, solved);
                }

                subTotal += solution[0, 0] * 100 + solution[1, 0] * 10 + solution[2, 0];

                return subTotal;
            },
            subTotal => Interlocked.Add(ref sum, subTotal));

        _stopwatch.Stop();

        Console.WriteLine($"\n All puzzles solved in: {_stopwatch.Elapsed.Minutes:N0}:{_stopwatch.Elapsed.Seconds:D2}.\n\n\n\n\n");
        
        Console.CursorVisible = true;
        
        return sum.ToString("N0");
    }

    private void Dump(int[,] left, int[,] right, int solved)
    {
        Console.CursorTop = 1;
        
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                if (left[x, y] == 0)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write($" {left[x, y]}");
                }
            }

            Console.Write("    ");
            
            for (var x = 0; x < 9; x++)
            {
                Console.Write($" {right[x, y]}");
            }
            
            Console.WriteLine();
        }

        Console.WriteLine($"\n Solved: {solved:N0}/{Input.Length:N0} puzzles.\n");

        var mean = _elapsed.Total / solved;
        
        Console.WriteLine($" Timings...\n  Minimum: {_elapsed.Minimum:N0}μs          \n  Mean:    {mean:N0}μs          \n  Maximum: {_elapsed.Maximum:N0}μs          \n");
        
        Console.WriteLine($" Combinations...\n  Minimum: {_steps.Minimum:N0}          \n  Mean:    {_steps.Total / solved:N0}          \n  Maximum: {_steps.Maximum:N0} (Puzzle #{_maxPuzzleNumber}).           \n");

        var meanTime = _stopwatch.Elapsed.TotalSeconds / solved;
        
        var eta = TimeSpan.FromSeconds((Input.Length - solved) * meanTime);
        
        Console.WriteLine($" Elapsed time: {_stopwatch.Elapsed.Minutes:N0}:{_stopwatch.Elapsed.Seconds:D2}    Estimated remaining: {eta.Minutes:N0}:{eta.Seconds:D2}          \n");
        
        foreach (var item in _history.TakeLast(10).Reverse())
        {
            Console.WriteLine($" Puzzle #{item.Id} solved in {item.Elapsed:N0}μs.          ");
        }
    }

    private int[,] Solve(int id, int[,] sudoku)
    {
        var queue = new PriorityQueue<int[,], int>();

        var queueLock = new object();
        
        queue.Enqueue(sudoku, 0);

        var complete = false;

        int[,] answer = null;

        var steps = 0;
        
        var worker = () =>
        {
            while (! complete)
            {
                int[,] puzzle;
                
                lock (queueLock)
                {
                    queue.TryDequeue(out puzzle, out _);

                    steps++;
                }

                if (puzzle != null)
                {
                    var solutions = SolveStep(puzzle);

                    foreach (var solution in solutions)
                    {
                        if (solution.Score == 0)
                        {
                            answer = solution.Sudoku;

                            complete = true;

                            lock (_statsLock)
                            {
                                _steps.Total += steps;

                                _steps.Minimum = Math.Min(_steps.Minimum, steps);

                                if (steps > _steps.Maximum)
                                {
                                    _steps.Maximum = steps;

                                    _maxPuzzleNumber = id;
                                }
                            }
                        }

                        lock (queueLock)
                        {
                            queue.Enqueue(solution.Sudoku, solution.Score);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(0);
                }
            }
        };
        
        Parallel.Invoke(worker, worker, worker, worker, worker, worker, worker, worker);

        return answer;
    }
    
    private static bool IsValid(int[,] sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            var rowSet = 0u;

            var columnSet = 0u;
            
            for (var x = 0; x < 9; x++)
            {
                var cell = sudoku[x, y];

                if (cell != 0)
                {
                    var bit = (uint) 1 << cell;

                    if ((rowSet & bit) != 0)
                    {
                        return false;
                    }

                    rowSet |= bit;
                }

                if (x == y)
                {
                    continue;
                }

                cell = sudoku[y, x];

                if (cell != 0)
                {
                    var bit = (uint) 1 << cell;

                    if ((columnSet & bit) != 0)
                    {
                        return false;
                    }

                    columnSet |= bit;
                }
            }
        }

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                var set = 0u;
                
                for (var x1 = 0; x1 < 3; x1++)
                {
                    for (var y1 = 0; y1 < 3; y1++)
                    {
                        var cell = sudoku[x * 3 + x1, y * 3 + y1];

                        if (cell != 0)
                        {
                            var value = (uint) 1 << cell;

                            if ((set & value) != 0)
                            {
                                return false;
                            }

                            set |= value;
                        }
                    }
                }
            }
        }

        return true;
    }

    private static List<(int[,] Sudoku, int Score)> SolveStep(int[,] sudoku)
    {
        var rowCandidates = new int[9];
        
        var columnCandidates = new int[9];
        
        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = 0b11_1111_1111;

            columnCandidates[y] = 0b11_1111_1111;
            
            for (var x = 0; x < 9; x++)
            {
                rowCandidates[y] ^= 1 << sudoku[x, y];

                columnCandidates[y] ^= 1 << sudoku[y, x];
            }
        }

        var boxCandidates = new int[9];

        for (var y = 0; y < 3; y++) 
        {
            for (var x = 0; x < 3; x++)
            {
                boxCandidates[y * 3 + x] = 0b11_1111_1111;

                for (var y1 = 0; y1 < 3; y1++)
                {
                    for (var x1 = 0; x1 < 3; x1++)
                    {
                        boxCandidates[y * 3 + x] ^= 1 << sudoku[x * 3 + x1, y * 3 + y1];
                    }
                }
            }
        }

        var position = (X: -1, Y: -1);

        var values = 0;

        var valueCount = 0b11_1111_1111;
        
        for (var y = 0; y < 9; y++)
        {
            var row = rowCandidates[y];

            if (row == 0)
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

                if (column == 0)
                {
                    continue;
                }

                var box = boxCandidates[y / 3 * 3 + x / 3];
                
                var common = row & column & box;

                var count = BitOperations.PopCount((uint) common);
                
                if (count < valueCount)
                {
                    position = (x, y);

                    values = common;

                    valueCount = count;

                    if (count == 1)
                    {
                        goto next;
                    }
                }
            }
        }
        next:

        var solutions = new List<(int[,] Sudokus, int Score)>();

        for (var i = 1; i < 10; i++)
        {
            if ((values & (1 << i)) == 0)
            {
                continue;
            }

            sudoku[position.X, position.Y] = i;

            if (IsValid(sudoku))
            {
                var copy = new int[9, 9];

                var score = 81;
            
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

                solutions.Add((copy, score));
            }
        }
        
        return solutions;
    }

    private int[,] LoadSudoku(int number)
    {
        var matrix = new int[9, 9];

        var line = Input[number];
        
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                matrix[x, y] = line[y * 9 + x] - '0';
            }
        }

        return matrix;
    }
}