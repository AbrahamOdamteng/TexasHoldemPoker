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
    class StraightFlush :BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.StraightFlush; } }

        #endregion

        private StraightFlush(IEnumerable<Card> _cards)
        {
            Cards = _cards;
        }

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

                var res = new StraightFlush(straightFlushCards);
                return res;
            }

            return null;
        }

        internal static StraightFlush CreateInstance(IEnumerable<Card> cards)
        {
            if (!Utils.ConsequtiveCardsNoDuplicates(cards)) return null;
            if (!Utils.AllSameSuit(cards)) return null;
            if (Utils.AllRoyal(cards)) return null;//roayl flush, not straight flush

            return new StraightFlush(cards);
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

        #region Override Object methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion


        #region Equality Operators
        public static bool operator ==(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            if (straightFlush is null)
            {
                return pokerHand is null ? true : false;
            }
            return straightFlush.Equals(pokerHand);
        }

        public static bool operator !=(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            if (straightFlush is null)
            {
                return pokerHand is null ? false : true;
            }
            return !straightFlush.Equals(pokerHand);
        }
        #endregion


        #region Implementation of IComparable
        public override int CompareTo(IPokerHand other)
        {
            if (other is null) return 1;
            if (HandRank > other.HandRank) return 1;
            if (HandRank < other.HandRank) return -1;

            var otherHighCard = Utils.GetHighestCard(other.Cards);
            var myHighCard = Utils.GetHighestCard(Cards);

            if (myHighCard.Rank > otherHighCard.Rank) return 1;
            if (myHighCard.Rank < otherHighCard.Rank) return -1;
            return 0;

        }
        #endregion

        #region Comparison Operators

        public static bool operator >(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            return straightFlush.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            var res = straightFlush.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            return straightFlush.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(StraightFlush straightFlush, IPokerHand pokerHand)
        {
            var res = straightFlush.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }

        #endregion

    }
}
