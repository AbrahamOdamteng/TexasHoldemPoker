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

            throw new NotImplementedException();
        }
        #endregion

        #region operator overloading

        public static bool operator ==(Straight straight, IPokerHand pokerHand)
        {
            if (straight is null)
            {
                return pokerHand is null ? true : false;
            }
            return straight.Equals(pokerHand);
        }

        public static bool operator !=(Straight straight, IPokerHand pokerHand)
        {
            if (straight is null)
            {
                return pokerHand is null ? false : true;
            }
            return !straight.Equals(pokerHand);
        }

        public static bool operator >(Straight straight, IPokerHand pokerHand)
        {
            return straight.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(Straight straight, IPokerHand pokerHand)
        {
            var res = straight.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(Straight straight, IPokerHand pokerHand)
        {
            return straight.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(Straight straight, IPokerHand pokerHand)
        {
            var res = straight.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
