using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;

namespace TexasHoldem.Hands
{
    class HighCard : BaseHand
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

        public static bool operator ==(HighCard highCard, IPokerHand pokerHand)
        {
            if (highCard is null)
            {
                return pokerHand is null ? true : false;
            }
            return highCard.Equals(pokerHand);
        }

        public static bool operator !=(HighCard highCard, IPokerHand pokerHand)
        {
            if (highCard is null)
            {
                return pokerHand is null ? false : true;
            }
            return !highCard.Equals(pokerHand);
        }

        public static bool operator >(HighCard highCard, IPokerHand pokerHand)
        {
            return highCard.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(HighCard highCard, IPokerHand pokerHand)
        {
            var res = highCard.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(HighCard highCard, IPokerHand pokerHand)
        {
            return highCard.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(HighCard highCard, IPokerHand pokerHand)
        {
            var res = highCard.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
