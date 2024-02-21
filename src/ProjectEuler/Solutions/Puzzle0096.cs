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

            var solution = Solve(sudoku);
            
            sum += solution[0, 0] * 100 + solution[1, 0] * 10 + solution[2, 0];
        }

        return sum.ToString("N0");
    }

    private static int[,] Solve(int[,] sudoku)
    {
        var queue = new PriorityQueue<int[,], int>();
        
        queue.Enqueue(sudoku, 0);

        while (queue.TryDequeue(out var puzzle, out _))
        {
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
                    var bit = (uint) 1 << sudoku[y, x];

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
        var rowCandidates = new uint[9];
        
        var columnCandidates = new uint[9];
        
        for (var y = 0; y < 9; y++)
        {
            rowCandidates[y] = 0b11_1111_1111;

            columnCandidates[y] = 0b11_1111_1111;
            
            for (var x = 0; x < 9; x++)
            {
                var value = sudoku[x, y];

                rowCandidates[y] ^= (uint) 1 << value;
                
                value = sudoku[y, x];

                columnCandidates[y] ^= (uint) 1 << value;
            }
        }

        var position = (X: -1, Y: -1);

        var values = 0u;
        
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

                var common = row & column;

                var count = BitOperations.PopCount(common);
                
                if (count == 1)
                {
                    position = (x, y);

                    values = common;

                    goto next;
                }

                if (values == 0 || count < BitOperations.PopCount(values))
                {
                    position = (x, y);

                    values = common;
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