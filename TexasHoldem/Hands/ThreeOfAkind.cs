using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
namespace TexasHoldem.Hands
{
    class ThreeOfAkind : BaseHand
    {

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
