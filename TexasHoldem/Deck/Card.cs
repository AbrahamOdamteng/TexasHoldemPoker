using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.Deck
{
    struct Card : IEquatable<Card>, IComparable<Card>
    {
        internal CardRanks Rank { get; }
        internal CardSuits Suit { get; }

        public Card(string card)
        {
            card = card.Trim();
            card = card.Replace(" ", string.Empty);

            if (card.Length != 2)
            {
                var msg = "The string representation of a card must be exactly two characters";
                throw new ArgumentException(msg);
            }

            var r = card.First();
            var rank = char.IsDigit(r) ? "_" + r : r.ToString();

            CardRanks cardRank;
            if (!Enum.TryParse(rank, out cardRank))
            {
                var msg = "Enum 'CardRanks' does not contain value: " + r;
                throw new ArgumentException(msg);
            }
            Rank = cardRank;

            var s = card.Last();
            CardSuits cardSuit;
            if (!Enum.TryParse(s.ToString(), true, out cardSuit))
            {
                var msg = "Enum 'CardSuits' does not contain value: " + s;
                throw new ArgumentException(msg);
            }
            Suit = cardSuit;
            _isRoyal = null;
        }

        bool? _isRoyal;
        internal bool IsRoyal()
        {

            if (_isRoyal != null) return (bool)_isRoyal;

            var royals = new[] { CardRanks.T, CardRanks.J, CardRanks.Q, CardRanks.K, CardRanks.A };
            if (royals.Contains(Rank))
            {
                _isRoyal = true;
            }
            else
            {
                _isRoyal = false;
            }
            return (bool)_isRoyal;
        }

        public override string ToString()
        {
            var r = Rank.ToString();
            var rank = r.Length > 1 ? r.Substring(1) : r;
            return rank + Suit;
        }

        #region override Object Methods

        public override bool Equals(object obj)
        {
            if (!(obj is Card)) return false;
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + Rank.GetHashCode();
            hash = hash * 23 + Suit.GetHashCode();
            return hash;
        }

        #endregion

        #region IEquatable

        public bool Equals(Card other)
        {
            return Rank == other.Rank && Suit == other.Suit;
        }

        public static bool operator ==(Card cardA, Card cardB)
        {
            return cardA.Equals(cardB);
        }

        public static bool operator !=(Card cardA, Card cardB)
        {
            return !(cardA.Equals(cardB));
        }

        #endregion

        #region IComparable

        public int CompareTo(Card other)
        {
            if (other == null) return 1;
            if (Rank < other.Rank)
            {
                return -1;
            }
            else if (Rank == other.Rank)
            {
                return -0;
            }
            else
            {
                return 1;
            }
        }

        public static bool operator > (Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) == 1;
        }

        public static bool operator >=(Card cardA, Card cardB)
        {
            var res = cardA.CompareTo(cardB);
            return res == 0 || res == 1;
        }

        public static bool operator <(Card cardA, Card cardB)
        {
            return cardA.CompareTo(cardB) == -1;
        }

        public static bool operator <=(Card cardA, Card cardB)
        {
            var res = cardA.CompareTo(cardB);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
