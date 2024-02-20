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

            Solve(sudoku);

            sum += sudoku[0, 0] * 100 + sudoku[1, 0] * 10 + sudoku[2, 0];
        }

        return sum.ToString("N0");
    }

    private static void Solve(int[,] sudoku)
    {
        var puzzle = new int[9, 9];
        
        Array.Copy(sudoku, puzzle, 81);

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

        for (var y = 0; y < 9; y++)
        {
            var row = rowCandidates[y];

            for (var x = 0; x < 9; x++)
            {
                var column = columnCandidates[x];

                var common = row.Intersect(column);
            }
        }
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