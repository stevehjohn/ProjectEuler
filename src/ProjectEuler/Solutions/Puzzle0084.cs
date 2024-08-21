using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0084 : Puzzle
{
    private const int DiceSides = 6;
    
    private readonly int[] _squareLandings = new int[40];

    private static readonly Dictionary<int, string> SpecialSqaures = new()
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

        for (var i = 0; i < 1_000_000; i++)
        {
            PlayRound();
        }

        throw new NotImplementedException();
    }

    private void PlayRound()
    {
        int roll1;
        int roll2;

        var rolls = 1;
        
        do
        {
            if (rolls == 3)
            {
                SetPosition("JL");
                
                break;
            }

            roll1 = _rng.Next(DiceSides) + 1;
            roll2 = _rng.Next(DiceSides) + 1;

            _position += roll1 + roll2;

            if (_position > 39)
            {
                _position -= 39;
            }

            var turnOver = false;
            
            if (SpecialSqaures.TryGetValue(_position, out var square))
            {
                switch (square)
                {
                    case "GJ":
                        SetPosition("JL");
                        turnOver = true;

                        break;
                    
                    case "CC:":
                        turnOver = PickCard(_chestCards, ref _chestCard);

                        break;
                    
                    case "CH":
                        turnOver = PickCard(_chanceCards, ref _chanceCard);
                        
                        break;
                }
            }

            if (turnOver)
            {
                break;
            }

            rolls++;

        } while (roll1 == roll2);

        _squareLandings[_position]++;
    }

    private bool PickCard(string[] cards, ref int card)
    {
        var turnOver = false;

        switch (cards[card])
        {
            case "JL":
                SetPosition("JL");
                turnOver = true;
                
                break;
            
            case "GO":
                SetPosition("GO");
                
                break;
        }
        
        card++;

        if (card >= cards.Length)
        {
            card = 0;
        }

        return turnOver;
    }

    private void SetPosition(string position)
    {
        _position = SpecialSqaures.Single(s => s.Value == position).Key;
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