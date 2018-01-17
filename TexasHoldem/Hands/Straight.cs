using System;
using System.Collections.Generic;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
using TexasHoldem.Utilities;
namespace TexasHoldem.Hands
{
    class Straight : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.Straight; } }

        #endregion

        Straight(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        #region CreateInstance

        public static Straight CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static Straight CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);

            if (!Utils.ConsequtiveCardsNoDuplicates(cards)) return null;

            if (Utils.AllSameSuit(cards)) return null;//Cards represent a straight flush

            return new Straight(cards);
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

            var otherHighCard = Utils.GetHighestCard(other.Cards);
            var myHighCard = Utils.GetHighestCard(Cards);

            if (myHighCard.Rank > otherHighCard.Rank) return 1;
            if (myHighCard.Rank < otherHighCard.Rank) return -1;
            return 0;
        }
        #endregion
    }
}
