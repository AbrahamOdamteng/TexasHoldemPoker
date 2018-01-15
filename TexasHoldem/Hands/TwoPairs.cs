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
    class TwoPairs : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.TwoPairs; } }

        #endregion


        IEnumerable<Card> HighPair { get; }
        IEnumerable<Card> LowPair { get; }
        Card Kicker { get; }

        TwoPairs(IEnumerable<Card> highPair, IEnumerable<Card> lowPair, Card kicker)
        {
            var highPairCount = highPair.Count();
            if (highPairCount != 2)
            {
                var msg = $"Parameter 'highPair' should contain 3 elements, containts {highPairCount} elements";
                throw new ArgumentException(msg);
            }

            var lowPairCount = lowPair.Count();
            if (lowPairCount != 2)
            {
                var msg = $"Parameter 'lowPair' should contain 2 elements, containts {lowPairCount} elements";
                throw new ArgumentException(msg);
            }

            HighPair = highPair;
            LowPair = lowPair;
            Kicker = kicker;

            Cards = highPair.Concat(lowPair).Concat(new[] { Kicker });
        }

        #region CreateInstance

        public static TwoPairs CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            throw new NotImplementedException();
        }

        internal static TwoPairs CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);
            var rankGroups = cards.GroupBy(c => c.Rank);
            var thing = rankGroups.Where(g => g.Count() == 2);
            if (rankGroups.Where(g => g.Count() == 2).Count() != 2) return null;

            var pairs = rankGroups.Where(g => g.Count() == 2).OrderBy(g => g.Key);

            var lowPair = pairs.First();
            var highPair = pairs.Last();
            var kicker = rankGroups.Single(g => g.Count() == 1).Single();
            return new TwoPairs(highPair, lowPair, kicker);
        }
        #endregion

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
            if (other is null) return 1;
            if (HandRank > other.HandRank) return 1;
            if (HandRank < other.HandRank) return -1;
           

            throw new NotImplementedException();
        }
        #endregion
    }
}
