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
    class ThreeOfAkind : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.ThreeOfAkind; } }

        #endregion

        #region CreateInstance

        public static ThreeOfAkind CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static ThreeOfAkind CreateInstance(IEnumerable<Card> cards)
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

        public static bool operator ==(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            if (threeOfAkind is null)
            {
                return pokerHand is null ? true : false;
            }
            return threeOfAkind.Equals(pokerHand);
        }

        public static bool operator !=(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            if (threeOfAkind is null)
            {
                return pokerHand is null ? false : true;
            }
            return !threeOfAkind.Equals(pokerHand);
        }

        public static bool operator >(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            return threeOfAkind.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            var res = threeOfAkind.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            return threeOfAkind.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(ThreeOfAkind threeOfAkind, IPokerHand pokerHand)
        {
            var res = threeOfAkind.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
