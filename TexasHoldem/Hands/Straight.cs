using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Utilities;
using TexasHoldem.Interfaces;
namespace TexasHoldem.Hands
{
    class Straight : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.Straight; } }

        #endregion

        #region CreateInstance

        public static Straight CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static Straight CreateInstance(IEnumerable<Card> cards)
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
