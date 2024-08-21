using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0084 : Puzzle
{
    private const int DiceSides = 6;
    
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

    private int _chestCard;

    private int _chanceCard;

    private readonly Random _rng = new();

    private int _position;
    
    public override string GetAnswer()
    {
        InitialiseGame();
        
        throw new NotImplementedException();
    }

    private void PlayRound()
    {
        int roll1;
        int roll2;
        
        do
        {
            roll1 = _rng.Next(DiceSides) + 1;
            roll2 = _rng.Next(DiceSides) + 1;

            _position += roll1 + roll2;

            if (_position > 39)
            {
                _position -= 39;
            }

            if (_specialSqaures.TryGetValue(_position, out var sqaure))
            {
                switch (sqaure)
                {
                    case "JL":
                        
                }
            }

        } while (roll1 == roll2);
    }
    
    private void InitialiseGame()
    {
        _chestCards = new string[16];

        _chanceCards = new string[16];

        _chanceCard = 0;

        _chanceCard = 0;
        
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

        _position = 0;
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