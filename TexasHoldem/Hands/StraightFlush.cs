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
    class StraightFlush : IPokerHand
    {
        public HandRanks HandRank { get { return HandRanks.StraightFlush; } }

        public IEnumerable<Card> Cards { get; private set; }

        private StraightFlush() { }

        public static StraightFlush CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);

            IEnumerable<Card> straightFlushCards;

            if (GetHighestStraightFlush(communityCards).Any())
            {
                var allCards = communityCards.Concat(holeCards);
                straightFlushCards = GetHighestStraightFlush(allCards);

                if (communityCards.OrderBy(c => c.Rank).SequenceEqual(straightFlushCards.OrderBy(c => c.Rank)))
                {
                    var filteredCards = allCards.ToList();
                    filteredCards.Remove(communityCards.OrderBy(c => c.Rank).Last());
                    straightFlushCards = GetHighestStraightFlush(filteredCards);
                }
            }
            else
            {
                var allCards = communityCards.Concat(holeCards);
                straightFlushCards = GetHighestStraightFlush(allCards);

                if (!straightFlushCards.Any()) return null;
            }

            if (straightFlushCards.Any())
            {
                //This hand is actually a royal flush
                if (straightFlushCards.All(c => c.IsRoyal())) return null;

                var res = new StraightFlush();
                res.Cards = straightFlushCards;
                return res;
            }

            return null;
        }

        internal static StraightFlush CreateInstance(IEnumerable<Card> cards)
        {
            return null;
        }


        static IEnumerable<Card> GetHighestStraightFlush(IEnumerable<Card> cards)
        {
            var candidateCards = cards.GroupBy(c => c.Suit)
                .Where(g => g.Count() > 4)
                .FirstOrDefault();

            if (candidateCards == null) return Enumerable.Empty<Card>();

            var res = Utils.GetHighestStraight(candidateCards.ToArray());

            return res;

        }

        public int CompareTo(IPokerHand other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
