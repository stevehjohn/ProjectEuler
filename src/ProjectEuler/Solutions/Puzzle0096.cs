using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0096 : Puzzle
{
    private readonly int[] _rowCandidates = new int[9];
    
    private readonly int[] _columnCandidates = new int[9];
    
    private readonly int[] _boxCandidates = new int[9];

    private readonly int[] _cellCandidates = new int[81];

    public override string GetAnswer()
    {
        LoadInput();

        var sum = 0;

        for (var i = 0; i < 50; i++)
        {
            var sudoku = LoadSudoku(i);

            var solution = Solve(sudoku);

            sum += solution[0] * 100 + solution[1] * 10 + solution[2];
        }
        
        return sum.ToString("N0");
    }

    private int[] Solve(int[] puzzle)
    {
        var workingCopy = new int[81];
        
        var score = 81;
        
        for (var i = 0; i < 81; i++)
        {
            if (puzzle[i] != 0)
            {
                score--;

                workingCopy[i] = puzzle[i];
            }
        }

        var span = new Span<int>(workingCopy);
        
        GetCellCandidates(span);

        SolveStep(span, score);
        
        return workingCopy;
    }
    
    private bool SolveStep(Span<int> puzzle, int score)
    {
        FindHiddenSingles();
        
        var move = FindLowestMove(puzzle);

        return CreateNextSteps(puzzle, move, score);
    }

    private void GetCellCandidates(Span<int> puzzle)
    {
        for (var y = 0; y < 9; y++)
        {
            _rowCandidates[y] = 0b11_1111_1111;

            _columnCandidates[y] = 0b11_1111_1111;

            var y9 = (y << 3) + y;

            for (var x = 0; x < 9; x++)
            {
                _rowCandidates[y] &= ~(1 << puzzle[x + y9]);

                _columnCandidates[y] &= ~(1 << puzzle[y + (x << 3) + x]);
            }
        }

        var boxIndex = 0;
        
        for (var yO = 0; yO < 81; yO += 27)
        {
            for (var xO = 0; xO < 9; xO += 3)
            {
                var start = xO + yO;

                _boxCandidates[boxIndex] = 0b11_1111_1111;

                for (var y = 0; y < 3; y++)
                {
                    var row = start + (y << 3) + y;

                    for (var x = 0; x < 3; x++)
                    {
                        _boxCandidates[boxIndex] &= ~(1 << puzzle[row + x]);
                    }
                }

                boxIndex++;
            }
        }

        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                if (puzzle[x + (y << 3) + y] == 0)
                {
                    _cellCandidates[x + (y << 3) + y] = _columnCandidates[x] & _rowCandidates[y] & _boxCandidates[y / 3 * 3 + x / 3];
                }
                else
                {
                    _cellCandidates[x + (y << 3) + y] = 0;
                }
            }
        }
    }

    private void FindHiddenSingles()
    {
        for (var y = 0; y < 9; y++)
        {
            var oneMaskRow = 0;
        
            var twoMaskRow = 0;
        
            var oneMaskColumn = 0;
        
            var twoMaskColumn = 0;
        
            for (var x = 0; x < 9; x++)
            {
                twoMaskRow |= oneMaskRow & _cellCandidates[(y << 3) + y + x];
        
                oneMaskRow |= _cellCandidates[(y << 3) + y + x];

                twoMaskColumn |= oneMaskColumn & _cellCandidates[(x << 3) + x + y];
        
                oneMaskColumn |= _cellCandidates[(x << 3) + x + y];
            }
        
            var onceRow = oneMaskRow & ~twoMaskRow;
        
            var onceColumn = oneMaskColumn & ~twoMaskColumn;
        
            if (BitOperations.PopCount((uint) onceRow) == 1)
            {
                for (var x = 0; x < 9; x++)
                {
                    if ((_cellCandidates[(y << 3) + y + x] & onceRow) > 0)
                    {
                        _cellCandidates[(y << 3) + y + x] = onceRow;

                        return;
                    }
                }
            }
        
            if (BitOperations.PopCount((uint) onceColumn) == 1)
            {
                for (var x = 0; x < 9; x++)
                {
                    if ((_cellCandidates[(x << 3) + x + y] & onceColumn) > 0)
                    {
                        _cellCandidates[(x << 3) + x + y] = onceColumn;

                        return;
                    }
                }
            }
        }

        for (var yO = 0; yO < 81; yO += 27)
        {
            for (var xO = 0; xO < 9; xO += 3)
            {
                var oneMask = 0;

                var twoMask = 0;

                var start = yO + xO;

                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        twoMask |= oneMask & _cellCandidates[start + (y << 3) + y + x];

                        oneMask |= _cellCandidates[start + (y << 3) + y + x];
                    }
                }

                var once = oneMask & ~twoMask;

                if (BitOperations.PopCount((uint) once) == 1)
                {
                    for (var y = 0; y < 3; y++)
                    {
                        for (var x = 0; x < 3; x++)
                        {
                            if ((_cellCandidates[start + (y << 3) + y + x] & once) > 0)
                            {
                                _cellCandidates[start + (y << 3) + y + x] = once;
                            }
                        }
                    }
                }
            }
        }
    }
    
    private ((int X, int Y) Position, int Values, int ValueCount) FindLowestMove(Span<int> puzzle)
    {
        var position = (X: -1, Y: -1);

        var values = 0;

        var valueCount = 0b11_1111_1111;

        for (var y = 0; y < 9; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                if (puzzle[x + (y << 3) + y] != 0)
                {
                    continue;
                }

                var candidates = _cellCandidates[x + (y << 3) + y];
                
                var count = BitOperations.PopCount((uint) candidates);

                if (count < valueCount)
                {
                    position = (x, y);

                    values = candidates;

                    valueCount = count;

                    if (count == 1)
                    {
                        return (position, values, valueCount);
                    }
                }
            }
        }

        return (position, values, valueCount);
    }

    private bool CreateNextSteps(Span<int> puzzle, ((int X, int Y) Position, int Values, int ValueCount) move, int score)
    {
        for (var i = 1; i < 10; i++)
        {
            var bit = 1 << i;
            
            if ((move.Values & bit) == 0)
            {
                continue;
            }

            puzzle[move.Position.X + (move.Position.Y << 3) + move.Position.Y] = i;

            var copy = new int[81];
            
            Array.Copy(_cellCandidates, copy, 81);

            var box = move.Position.Y / 3 * 27 + move.Position.X / 3 * 3;
            
            for (var j = 0; j < 9; j++)
            {
                _cellCandidates[j + move.Position.Y * 9] &= ~bit;
                
                _cellCandidates[move.Position.X + j * 9] &= ~bit;
                
                _cellCandidates[box + j % 3 + j / 3 * 9] &= ~bit;
            }

            score--;

            if (score == 0)
            {
                return true;
            }
            
            if (SolveStep(puzzle, score))
            {
                return true;
            }

            puzzle[move.Position.X + (move.Position.Y << 3) + move.Position.Y] = 0;

            Array.Copy(copy, _cellCandidates, 81);

            score++;
        }

        return false;
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