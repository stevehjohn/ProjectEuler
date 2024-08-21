using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0084 : Puzzle
{
    private int[] _squareLandings = new int[40];

    private static readonly Dictionary<int, string> _specialSqaures = new()
    {
        { 0, "GO" },
        { 2, "CC" },
        { 5, "R1" },
        { 7, "CH" },
        { 10, "JL" },
        { 11, "C1" },
        { 12, "U1" },
        { 15, "R2" },
        { 17, "CC" },
        { 22, "CH" },
        { 24, "E3" },
        { 25, "R3" },
        { 28, "U2" },
        { 30, "GJ" },
        { 33, "CC" },
        { 35, "R4" },
        { 36, "CH" },
        { 38, "H2" }
    };

    private string[] _chestCards;

    private string[] _chanceCards;

    private readonly Random _rng = new();
    
    public override string GetAnswer()
    {
        InitialiseGame();
        
        throw new NotImplementedException();
    }

    private void InitialiseGame()
    {
        _chestCards = new string[16];

        _chanceCards = new string[16];
        
        PlaceCard(_chestCards, "GO");   
        PlaceCard(_chestCards, "JL");
        
        PlaceCard(_chanceCards, "GO");
        PlaceCard(_chanceCards, "JL");
        PlaceCard(_chanceCards, "C1");
        PlaceCard(_chanceCards, "E3");
        PlaceCard(_chanceCards, "H2");
        PlaceCard(_chanceCards, "R1");
        PlaceCard(_chanceCards, "NR");
        PlaceCard(_chanceCards, "NR");
        PlaceCard(_chanceCards, "NU");
        PlaceCard(_chanceCards, "B3");
    }

    private void PlaceCard(string[] set, string card)
    {
        while (true)
        {
            var location = _rng.Next(16);

            if (set[location] == null)
            {
                set[location] = card;
                
                break;
            }
        }
    }
}