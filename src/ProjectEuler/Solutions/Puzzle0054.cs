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

        var player1Wins = 0;
        
        foreach (var line in Input)
        {
            var player1 = ParseHand(line[..14]);

            var player2 = ParseHand(line[15..]);

            if (player1.Score() > player2.Score())
            {
                player1Wins++;
            }
        }

        return player1Wins.ToString("N0");
    }

    private static Hand ParseHand(string hand)
    {
        var cards = new List<Card>();

        var parts = hand.Split(' ');

        foreach (var part in parts)
        {
            var suit = part[1] switch
            {
                'C' => Suite.Clubs,
                'D' => Suite.Diamonds,
                'H' => Suite.Hearts,
                'S' => Suite.Spades,
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
        private const long ScoreBoundary = 1_00_00_00_00_00;
        
        private readonly Card[] _cards;

        public Hand(List<Card> cards)
        {
            _cards = cards.OrderByDescending(c => c.Value).ToArray();
        }

        public long Score()
        {
            if (ConsecutiveSameSuite())
            {
                if (_cards[0].Value == Value.Ten)
                {
                    return ScoreBoundary * 10;
                }

                return ScoreBoundary * 9 + CardValues();
            }

            var matchCounts = CountMatches();

            if (matchCounts[0] == 4)
            {
                return ScoreBoundary * 8 + 100 * (int) _cards[0].Value + (int) _cards[4].Value;
            }

            if (matchCounts[1] == 4)
            {
                return ScoreBoundary * 8 + 100 * (int) _cards[1].Value + (int) _cards[0].Value;
            }

            if (matchCounts[0] == 3 && matchCounts[1] == 2)
            {
                return ScoreBoundary * 7 + 100 * (int) _cards[0].Value + (int) _cards[3].Value;
            }
            
            if (matchCounts[1] == 2 && matchCounts[0] == 3)
            {
                return ScoreBoundary * 7 + 100 * (int) _cards[3].Value + (int) _cards[0].Value;
            }

            if (SameSuit())
            {
                return ScoreBoundary * 6 + CardValues();
            }

            if (Consecutive())
            {
                return ScoreBoundary * 5 + CardValues();
            }

            if (matchCounts[0] == 3 && matchCounts[1] == 1)
            {
                return ScoreBoundary * 4 + 100 * (int) _cards[3].Value + (int) _cards[4].Value;
            }

            if (matchCounts[1] == 3 && matchCounts[0] == 1)
            {
                return ScoreBoundary * 4 + 100 * (int) _cards[0].Value + (int) _cards[4].Value;
            }

            if (matchCounts[2] == 3 && matchCounts[0] == 1)
            {
                return ScoreBoundary * 4 + 100 * (int) _cards[0].Value + (int) _cards[1].Value;
            }

            if (matchCounts[0] == 2 && matchCounts[1] == 2)
            {
                return ScoreBoundary * 3 + 10_000 * (int) _cards[0].Value + 100 * (int) _cards[2].Value + (int) _cards[4].Value;
            }

            if (matchCounts[0] == 2 && matchCounts[2] == 2)
            {
                return ScoreBoundary * 3 + 10_000 * (int) _cards[0].Value + 100 * (int) _cards[3].Value + (int) _cards[2].Value;
            }

            if (matchCounts[1] == 2 && matchCounts[2] == 2)
            {
                return ScoreBoundary * 3 + 10_000 * (int) _cards[1].Value + 100 * (int) _cards[3].Value + (int) _cards[0].Value;
            }

            if (matchCounts[0] == 2 && matchCounts[1] == 1 && matchCounts[2] == 1 && matchCounts[3] == 1)
            {
                return ScoreBoundary * 2 + 1_000_000 * (int) _cards[0].Value + 10_000 * (int) _cards[2].Value + 100 * (int) _cards[3].Value + (int) _cards[4].Value;
            }

            if (matchCounts[1] == 2 && matchCounts[0] == 1 && matchCounts[2] == 1 && matchCounts[3] == 1)
            {
                return ScoreBoundary * 2 + 1_000_000 * (int) _cards[1].Value + 10_000 * (int) _cards[0].Value + 100 * (int) _cards[3].Value + (int) _cards[4].Value;
            }

            if (matchCounts[2] == 2 && matchCounts[0] == 1 && matchCounts[1] == 1 && matchCounts[3] == 1)
            {
                return ScoreBoundary * 2 + 1_000_000 * (int) _cards[2].Value + 10_000 * (int) _cards[0].Value + 100 * (int) _cards[1].Value + (int) _cards[4].Value;
            }

            if (matchCounts[3] == 2 && matchCounts[0] == 1 && matchCounts[1] == 1 && matchCounts[2] == 1)
            {
                return ScoreBoundary * 2 + 1_000_000 * (int) _cards[3].Value + 10_000 * (int) _cards[0].Value + 100 * (int) _cards[1].Value + (int) _cards[2].Value;
            }

            return CardValues();
        }

        private long CardValues()
        {
            var values = 0L;
            
            var multiplier = ScoreBoundary / 100;

            for (var i = 0; i < 5; i++)
            {
                values += multiplier * (long) _cards[i].Value;

                multiplier /= 100;
            }

            return values;
        }

        private List<int> CountMatches()
        {
            var matchCounts = new Dictionary<Value, int>();

            foreach (var card in _cards)
            {
                if (! matchCounts.TryAdd(card.Value, 1))
                {
                    matchCounts[card.Value]++;
                }
            }

            return matchCounts.Select(c => c.Value).ToList();
        }

        private bool ConsecutiveSameSuite()
        {
            var current = _cards[0];
            
            for (var i = 1; i < 5; i++)
            {
                if (_cards[i].Value != current.Value + 1 || _cards[i].Suite != current.Suite)
                {
                    return false;
                }

                current = _cards[i];
            }

            return true;
        }

        private bool Consecutive()
        {
            var current = _cards[0];
            
            for (var i = 1; i < 5; i++)
            {
                if (_cards[i].Value != current.Value + 1)
                {
                    return false;
                }

                current = _cards[i];
            }

            return true;
        }

        private bool SameSuit()
        {
            var current = _cards[0];
            
            for (var i = 1; i < 5; i++)
            {
                if (_cards[i].Suite != current.Suite)
                {
                    return false;
                }

                current = _cards[i];
            }

            return true;
        }
    }

    private enum Suite
    {
        Clubs,
        Hearts,
        Diamonds,
        Spades
    }

    private enum Value
    {
        // ReSharper disable UnusedMember.Local
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        //  ReSharper restore UnusedMember.Local
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    private class Card
    {
        public Suite Suite { get; init; }

        public Value Value { get; init; }

        public Card(Suite suit, Value value)
        {
            Suite = suit;
            
            Value = value;
        }
    }
}