using System.Diagnostics;
using System.Numerics;
using System.Text;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    private readonly List<(int Id, double Elapsed, int Solved)> _history = new();

    private (double Total, double Minimum, double Maximum) _elapsed = (0, double.MaxValue, 0);

    private (long Total, int Minimum, int Maximum) _steps = (0, int.MaxValue, 0);

    private int _maxStepsPuzzleNumber;

    private int _maxTimePuzzleNumber;

    private Stopwatch _stopwatch;

    private readonly object _statsLock = new();

    private readonly StringBuilder _output = new(10_000);
    
    public override string GetAnswer()
    {
        LoadInput();
        
        var sum = 0;

        var solved = 0;
        
        Console.Clear();

        Console.CursorVisible = false;
        
        _stopwatch = Stopwatch.StartNew();
        
        Parallel.For(0, Input.Length,
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 },
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

                    if (stopwatch.Elapsed.TotalMicroseconds > _elapsed.Maximum)
                    {
                        _maxTimePuzzleNumber = i;
                        
                        _elapsed.Maximum = stopwatch.Elapsed.TotalMicroseconds;
                    }

                    solved++;

                    Dump(sudoku.Span, solution, solved);
                }

                subTotal += solution[0] * 100 + solution[1] * 10 + solution[2];

                return subTotal;
            },
            subTotal => Interlocked.Add(ref sum, subTotal));

        _stopwatch.Stop();

        Console.WriteLine($"\n All puzzles solved in: {_stopwatch.Elapsed.Minutes:N0}:{_stopwatch.Elapsed.Seconds:D2}.\n\n\n\n\n");
        
        Console.CursorVisible = true;
        
        return sum.ToString("N0");
    }

    private void Dump(Span<int> left, Span<int> right, int solved)
    {
        _output.Clear();
        
        Console.CursorTop = 1;
        
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                if (left[x + y * 9] == 0)
                {
                    _output.Append("  ");
                }
                else
                {
                    _output.Append($" {left[x + y * 9]}");
                }
            }

            _output.Append("    ");
            
            for (var x = 0; x < 9; x++)
            {
                _output.Append($" {right[x + y * 9]}");
            }
            
            _output.AppendLine();
        }

        _output.AppendLine($"\n Solved: {solved:N0}/{Input.Length:N0} puzzles ({solved / _stopwatch.Elapsed.TotalSeconds:N0} puzzles/sec).       \n");

        var mean = _elapsed.Total / solved;
        
        _output.AppendLine($" Timings...\n  Minimum: {_elapsed.Minimum:N0}μs          \n  Mean:    {mean:N0}μs          \n  Maximum: {_elapsed.Maximum:N0}μs (Puzzle #{_maxTimePuzzleNumber:N0})         \n");
        
        _output.AppendLine($" Combinations...\n  Minimum: {_steps.Minimum:N0}          \n  Mean:    {_steps.Total / solved:N0}          \n  Maximum: {_steps.Maximum:N0} (Puzzle #{_maxStepsPuzzleNumber:N0})           \n");

        var meanTime = _stopwatch.Elapsed.TotalSeconds / solved;
        
        var eta = TimeSpan.FromSeconds((Input.Length - solved) * meanTime);
        
        _output.AppendLine($" Elapsed time: {_stopwatch.Elapsed.Minutes:N0}:{_stopwatch.Elapsed.Seconds:D2}    Estimated remaining: {eta.Minutes:N0}:{eta.Seconds:D2}          \n");
        
        var percent = 100 - (Input.Length - solved) * 100d / Input.Length;

        _output.AppendLine($" Solved: {Math.Floor(percent):N0}%\n");

        var line = (int) Math.Floor(percent / 2);

        if (Math.Floor(percent) > 0 && (int) Math.Floor(percent) % 2 == 1)
        {
            _output.AppendLine($" {new string('\u2588', line)}\u258c{new string('⁃', 49 - line)}\u258f\n");
        }
        else
        {
            _output.AppendLine($" {new string('\u2588', line)}{new string('⁃', 50 - line)}\u258f\n");
        }

        foreach (var item in _history.TakeLast(10).Reverse())
        {
            _output.AppendLine($" Puzzle #{item.Id:N0} solved in {item.Elapsed:N0}μs.          ");
        }
        
        Console.Write(_output.ToString());
    }

    private int[] Solve(int id, Memory<int> sudoku)
    {
        var queue = new PriorityQueue<Memory<int>, int>();

        queue.Enqueue(sudoku, 0);

        var steps = 0;
        
        while (queue.TryDequeue(out var puzzle, out _))
        {
            steps++;

            var solutions = SolveStep(puzzle.Span);

            foreach (var solution in solutions)
            {
                if (solution.Solved)
                {
                    lock (_statsLock)
                    {
                        _steps.Total += steps;

                        _steps.Minimum = Math.Min(_steps.Minimum, steps);

                        if (steps > _steps.Maximum)
                        {
                            _steps.Maximum = steps;

                            _maxStepsPuzzleNumber = id;
                        }
                    }

                    return solution.Sudoku;
                }

                queue.Enqueue(solution.Sudoku, solution.Score);
            }
        }

        return null;
    }
    
    private static bool IsValid(Span<int> sudoku)
    {
        for (var y = 0; y < 9; y++)
        {
            var rowSet = 0u;

            var columnSet = 0u;

            var y9 = y * 9;
            
            for (var x = 0; x < 9; x++)
            {
                var cell = sudoku[x + y9];

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

                cell = sudoku[y + x * 9];

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

        for (var x = 0; x < 9; x += 3)
        {
            for (var y = 0; y < 9; y += 3)
            {
                var set = 0u;
                
                for (var x1 = 0; x1 < 3; x1++)
                {
                    for (var y1 = 0; y1 < 3; y1++)
                    {
                        var cell = sudoku[x + x1 + (y + y1) * 9];

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

    private static List<(int[] Sudoku, int Score, bool Solved)> SolveStep(Span<int> sudoku)
    {
        var rowCandidates = new int[9];
        
        var columnCandidates = new int[9];

        var frequencies = new int[10];
        
        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = 0b11_1111_1111;

            columnCandidates[y] = 0b11_1111_1111;

            var y9 = y * 9;
            
            for (var x = 0; x < 9; x++)
            {
                frequencies[sudoku[x + y9]]++;
                
                rowCandidates[y] &= ~(1 << sudoku[x + y9]);

                columnCandidates[y] &= ~(1 << sudoku[y + x * 9]);
            }
        }

        var boxCandidates = new int[9];

        for (var y = 0; y < 9; y += 3) 
        {
            for (var x = 0; x < 3; x++)
            {
                boxCandidates[y + x] = 0b11_1111_1111;

                var x3 = x * 3;
                
                for (var y1 = 0; y1 < 3; y1++)
                {
                    var yy1 = y + y1;
                    
                    for (var x1 = 0; x1 < 3; x1++)
                    {
                        boxCandidates[y + x] &= ~(1 << sudoku[x3 + x1 + yy1 * 9]);
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

            if (row == 1)
            {
                continue;
            }

            var y9 = y * 9; 

            for (var x = 0; x < 9; x++)
            {
                if (sudoku[x + y9] != 0)
                {
                    continue;
                }

                var column = columnCandidates[x];

                if (column == 1)
                {
                    continue;
                }

                var box = boxCandidates[y / 3 * 3 + x / 3];

                if (box == 1)
                {
                    continue;
                }

                var common = row & column & box;

                if (common == 1)
                {
                    continue;
                }

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

        var solutions = new List<(int[] Sudokus, int Score, bool Solved)>();

        for (var i = 1; i < 10; i++)
        {
            if ((values & (1 << i)) == 0)
            {
                continue;
            }

            sudoku[position.X + position.Y * 9] = i;

            if (IsValid(sudoku))
            {
                var copy = new int[81];

                var score = 81;
            
                for (var y = 0; y < 9; y++)
                {
                    for (var x = 0; x < 9; x++)
                    {
                        var value = sudoku[x + y * 9];

                        copy[x + y * 9] = value;

                        if (value != 0)
                        {
                            score--;
                        }
                    }
                }

                solutions.Add((copy, score * 100 + valueCount * 10 + frequencies[i], score == 0));

                if (score == 0)
                {
                    break;
                }
            }
        }
        
        return solutions;
    }

    private Memory<int> LoadSudoku(int number)
    {
        var puzzle = new int[81];

        var line = Input[number];
        
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                var c = line[y * 9 + x];

                if (c == '.')
                {
                    puzzle[x + y * 9] = 0;
                }
                else
                {
                    puzzle[x + y * 9] = line[y * 9 + x] - '0';
                }
            }
        }

        return puzzle;
    }
}