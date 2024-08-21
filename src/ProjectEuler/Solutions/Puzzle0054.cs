using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0054 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        foreach (var line in Input)
        {
            var player1 = ParseHand(line[..14]);

            var player2 = ParseHand(line[15..]);
        }
        
        throw new NotImplementedException();
    }

    private static List<Card> ParseHand(string hand)
    {
        return null;
    }

    private enum Suit
    {
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }

    private class Card
    {
        public Suit Suit { get; init; }

        public char Value { get; init; }

        public Card(Suit suit, char value)
        {
            Suit = suit;
            
            Value = value;
        }
    }
}