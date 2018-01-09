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
    class Flush : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.Flush; } }

        #endregion

        #region CreateInstance

        public static Flush CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static Flush CreateInstance(IEnumerable<Card> cards)
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

        public static bool operator ==(Flush flush, IPokerHand pokerHand)
        {
            if (flush is null)
            {
                return pokerHand is null ? true : false;
            }
            return flush.Equals(pokerHand);
        }

        public static bool operator !=(Flush flush, IPokerHand pokerHand)
        {
            if (flush is null)
            {
                return pokerHand is null ? false : true;
            }
            return !flush.Equals(pokerHand);
        }

        public static bool operator >(Flush flush, IPokerHand pokerHand)
        {
            return flush.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(Flush flush, IPokerHand pokerHand)
        {
            var res = flush.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(Flush flush, IPokerHand pokerHand)
        {
            return flush.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(Flush flush, IPokerHand pokerHand)
        {
            var res = flush.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
