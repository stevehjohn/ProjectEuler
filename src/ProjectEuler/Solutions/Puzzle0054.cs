using JetBrains.Annotations;
using ProjectEuler.Exceptions;
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

    private static Hand ParseHand(string hand)
    {
        var cards = new List<Card>();

        var parts = hand.Split(' ');

        foreach (var part in parts)
        {
            var suit = part[1] switch
            {
                'C' => Suit.Clubs,
                'D' => Suit.Diamonds,
                'H' => Suit.Hearts,
                'S' => Suit.Spades,
                _ => throw new PuzzleException($"Unrecognised suite {part[1]}.")
            };

            var value = part[0] switch
            {
                'J' => Value.Jack,
                'Q' => Value.Queen,
                'K' => Value.King,
                'A' => Value.Ace,
                'T' => Value.Ten,
                var v and >= '2' and <= '9' => (Value) v - '2',
                _ => throw new PuzzleException($"Unrecognised value {part[0]}.")
            };
            
            cards.Add(new Card(suit, value));
        }
        
        return new Hand(cards);
    }

    private class Hand
    {
        private IReadOnlyList<Card> Cards { get; init; }

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }
    }

    private enum Suit
    {
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }

    private enum Value
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    private class Card
    {
        public Suit Suit { get; init; }

        public Value Value { get; init; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            
            Value = value;
        }
    }
}