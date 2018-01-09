using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
namespace TexasHoldem.Hands
{
    class TwoPairs : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.TwoPairs; } }

        #endregion

        #region CreateInstance

        public static TwoPairs CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            throw new NotImplementedException();
        }

        internal static TwoPairs CreateInstance(IEnumerable<Card> cards)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion

        #region operator overloading

        public static bool operator ==(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            if (twoPairs is null)
            {
                return pokerHand is null ? true : false;
            }
            return twoPairs.Equals(pokerHand);
        }

        public static bool operator !=(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            if (twoPairs is null)
            {
                return pokerHand is null ? false : true;
            }
            return !twoPairs.Equals(pokerHand);
        }

        public static bool operator >(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            return twoPairs.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            var res = twoPairs.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            return twoPairs.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(TwoPairs twoPairs, IPokerHand pokerHand)
        {
            var res = twoPairs.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
