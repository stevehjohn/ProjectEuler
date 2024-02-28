using System.Buffers;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    private (double Total, double Minimum, double Maximum) _elapsed = (0, double.MaxValue, 0);

    private (long Total, int Minimum, int Maximum) _steps = (0, int.MaxValue, 0);

    private (long Total, int Minimum, int Maximum) _clues = (0, int.MaxValue, 0);

    private int _maxStepsPuzzleNumber;

    private int _maxTimePuzzleNumber;

    private Stopwatch _stopwatch;

    private readonly object _cluesLock = new();

    private readonly object _stepsLock = new();

    private readonly object _statsLock = new();

    private readonly object _consoleLock = new();

    private readonly StringBuilder _output = new(10_000);

    private readonly ArrayPool<int> _pool = ArrayPool<int>.Shared;

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
                    var totalMicroseconds = stopwatch.Elapsed.TotalMicroseconds;
                    
                    _elapsed.Total += totalMicroseconds;

                    _elapsed.Minimum = Math.Min(_elapsed.Minimum, totalMicroseconds);

                    if (totalMicroseconds > _elapsed.Maximum)
                    {
                        _maxTimePuzzleNumber = i;

                        _elapsed.Maximum = totalMicroseconds;
                    }

                    solved++;
                }

                Dump(sudoku, solution, solved);

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
        lock (_consoleLock)
        {
            _output.Clear();
        
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

            _output.AppendLine($" Clues...\n  Minimum: {_clues.Minimum:N0}          \n  Mean:    {_clues.Total / solved:N0}          \n  Maximum: {_clues.Maximum:N0}         \n");

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

            Console.CursorTop = 1;
        
            Console.Write(_output.ToString());
        }
    }

    private int[] Solve(int id, int[] sudoku)
    {
        var stack = new Stack<int[]>();

        stack.Push(sudoku);

        var steps = 0;
        
        while (stack.TryPop(out var puzzle))
        {
            steps++;

            var solutions = SolveStep(puzzle);

            if (steps > 1)
            {
                _pool.Return(puzzle);
            }

            foreach (var solution in solutions)
            {
                if (solution.Solved)
                {
                    lock (_stepsLock)
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

                stack.Push(solution.Sudoku);
            }
        }

        return null;
    }
    
    private List<(int[] Sudoku, bool Solved)> SolveStep(int[] sudoku)
    {
        var rowCandidates = new int[9];
        
        var columnCandidates = new int[9];

        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = 0b11_1111_1111;

            columnCandidates[y] = 0b11_1111_1111;

            var y9 = y * 9;
            
            for (var x = 0; x < 9; x++)
            {
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

                var box = boxCandidates[y / 3 * 3 + x / 3];

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
                }
            }
        }

        var solutions = new List<(int[] Sudoku, bool Solved)>();

        for (var i = 1; i < 10; i++)
        {
            if ((values & (1 << i)) == 0)
            {
                continue;
            }

            sudoku[position.X + position.Y * 9] = i;

            var copy = _pool.Rent(81);

            var score = 81;
        
            for (var j = 0; j < 81; j++)
            {
                var value = sudoku[j];

                copy[j] = value;

                if (value != 0)
                {
                    score--;
                }
            }

            if (score == 0)
            {
                solutions.Clear();

                solutions.Add((copy, true));
                
                break;
            }

            solutions.Add((copy, false));
        }
        
        return solutions;
    }

    private int[] LoadSudoku(int number)
    {
        var puzzle = new int[81];

        var line = Input[number];

        var clues = 0;
        
        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                var position = x + y * 9;
                
                var c = line[position];

                if (c == '.')
                {
                    puzzle[position] = 0;
                }
                else
                {
                    puzzle[position] = line[position] - '0';
                }

                if (puzzle[position] != 0)
                {
                    clues++;
                }
            }
        }

        lock (_cluesLock)
        {
            _clues.Total += clues;

            _clues.Minimum = Math.Min(_clues.Minimum, clues);

            _clues.Maximum = Math.Max(_clues.Maximum, clues);
        }

        return puzzle;
    }
}