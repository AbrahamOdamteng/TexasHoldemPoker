using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
using TexasHoldem.Utilities;

namespace TexasHoldem.Hands
{
    class RoyalFlush : IPokerHand
    {
        public IEnumerable<Card> Cards { get; private set; }

        public HandRanks HandRank => HandRanks.RoyalFlush;

        private RoyalFlush() { }

        public static RoyalFlush CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);

            if (GetRoyalFlush(communityCards).Any())
            {
                //If the RoyalFlush is in the community cards then it doesnt count.
                return null;
            }

            var allCards = new List<Card>(communityCards);
            allCards.AddRange(holeCards);

            var rfCards = GetRoyalFlush(allCards);
            if (!rfCards.Any()) return null;

            var royalflush = new RoyalFlush();
            royalflush.Cards = rfCards;

            return royalflush;
        }

        internal static RoyalFlush CreateInstance(IEnumerable<Card> cards)
        {
            var rfCards = GetRoyalFlush(cards);
            if (!rfCards.Any()) return null;

            var royalflush = new RoyalFlush();
            royalflush.Cards = rfCards;

            return royalflush;
        }

        static IEnumerable<Card> GetRoyalFlush(IEnumerable<Card> cards)
        {
            if (cards.Count() < 5) return Enumerable.Empty<Card>();

            var royals = cards.Where(c => c.IsRoyal());
            if (royals.Count() < 5) return Enumerable.Empty<Card>();

            return royals.GroupBy(r => r.Suit).Where(g => g.Count() == 5).SelectMany(g => g);
        }

        #region Override Object methods
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var pokerHand = obj as IPokerHand;
            if (pokerHand == null) return false;

            return Equals(pokerHand);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + (int)this.HandRank;
            foreach (var card in Cards)
            {
                hash = hash * 23 + card.GetHashCode();
            }
            return hash;
        }
        #endregion



        #region IEquatable

        public bool Equals(IPokerHand other)
        {
            if (other == null) return false;
            if (other.HandRank == HandRanks.RoyalFlush)
            {
                var othersuit = other.Cards.First().Suit;
                var mysuit = Cards.First().Suit;
                if (mysuit == othersuit) return true;

            }
            return false;
        }
        
        public static bool operator == (RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            return royalFlush.Equals(pokerHandB);
        }

        public static bool operator !=(RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            return !royalFlush.Equals(pokerHandB);
        }

        #endregion

        #region IComparable

        public int CompareTo(IPokerHand other)
        {
            if (other == null) return 1;
            if (other.HandRank == HandRanks.RoyalFlush) return 0;
            return -1;
        }

        public static bool operator > (RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            return royalFlush.CompareTo(pokerHandB) == 1;
        }

        public static bool operator >= (RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            var res = royalFlush.CompareTo(pokerHandB);
            return res == 0 || res == 1;
        }

        public static bool operator < (RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            return royalFlush.CompareTo(pokerHandB) == -1;
        }

        public static bool operator <= (RoyalFlush royalFlush, IPokerHand pokerHandB)
        {
            var res = royalFlush.CompareTo(pokerHandB);
            return res == 0 || res == -1;
        }
        #endregion










    }
}
