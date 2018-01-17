using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
using TexasHoldem.Utilities;

namespace TexasHoldem.Hands
{
    class FourOfAKind : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.FourOfAKind; } }

        #endregion

        public IEnumerable<Card> Quad { get; }
        public CardRanks QuadRank { get; }
        public Card Kicker { get; set; }

        private FourOfAKind(IEnumerable<Card> quad, Card kicker)
        {

            Quad = quad;
            QuadRank = quad.First().Rank;
            Kicker = kicker;
            Cards = quad.Concat(new[] { kicker }).ToArray();
            var count = Cards.Count();
            if(count != 5)
            {
                throw new ArgumentException($"A FourOfAKind hand consists of 5 cards, {count} given");
            }
        }

        public static FourOfAKind CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            if (!(GetHighestFourOfAKind(communityCards) is null))
            {
                return null;
            }

            var allCards = communityCards.Concat(holeCards);
            var fourOfAKindTuple = GetHighestFourOfAKind(allCards);
            if (fourOfAKindTuple is null)
            {
                return null;
            }
            return new FourOfAKind(fourOfAKindTuple.Item1, fourOfAKindTuple.Item2);
            
        }

        public static FourOfAKind CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);
            var quadsGroups = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 4);

            if (!quadsGroups.Any()) return null;
            var quads = quadsGroups.First();

            var kicker = cards.Single(c => c.Rank != quads.First().Rank);

            return new FourOfAKind(quads.ToArray(), kicker);
        }

        static Tuple<IEnumerable<Card>, Card> GetHighestFourOfAKind(IEnumerable<Card> cards)
        {
            var quad = cards.GroupBy(c => c.Rank).SingleOrDefault(g => g.Count() == 4);

            if (quad == null) return null;

            var highestKicker = cards.Where(c => !quad.Contains(c)).OrderBy(c => c.Rank).Last();

            return new Tuple<IEnumerable<Card>, Card>(quad, highestKicker);
        }


        #region override of Object Methods
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Implemenation of IComparable
        public override int CompareTo(IPokerHand other)
        {
            if (other is null) return -1;
            if (HandRank > other.HandRank) return 1;
            if (HandRank < other.HandRank) return -1;

            var otherHand = (FourOfAKind)ConvertToThisType(other);

            if (QuadRank > otherHand.QuadRank) return 1;
            if (QuadRank < otherHand.QuadRank) return -1;

            if (Kicker.Rank > otherHand.Kicker.Rank) return 1;
            if (Kicker.Rank < otherHand.Kicker.Rank) return -1;

            return 0;
        }
        #endregion
    }
}
