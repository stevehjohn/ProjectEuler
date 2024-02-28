using System.Buffers;
using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    private readonly ArrayPool<int> _pool = ArrayPool<int>.Shared;

    public override string GetAnswer()
    {
        LoadInput();
        
        var sum = 0;

        Parallel.For(0, Input.Length / 10,
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 },
            () => 0,
            (i, _, subTotal) => {
                var sudoku = LoadSudoku(i);

                var solution = Solve(sudoku);

                subTotal += solution[0] * 100 + solution[1] * 10 + solution[2];

                return subTotal;
            },
            subTotal => Interlocked.Add(ref sum, subTotal));
        
        return sum.ToString("N0");
    }

    private int[] Solve(int[] sudoku)
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
                    while (stack.TryPop(out puzzle))
                    {
                        _pool.Return(puzzle);
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

        var start = number * 10 + 1;
        
        for (var y = 0; y < 9; y++)
        {
            var line = Input[start + y];
        
            for (var x = 0; x < 9; x++)
            {
                puzzle[y * 9 + x] = line[x] - '0';
            }
        }

        return puzzle;
    }
}