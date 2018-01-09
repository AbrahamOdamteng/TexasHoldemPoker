﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;

namespace TexasHoldem.Hands
{
    class FullHouse : BaseHand
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

        public static bool operator ==(FullHouse fullHouse, IPokerHand pokerHand)
        {
            if (fullHouse is null)
            {
                return pokerHand is null ? true : false;
            }
            return fullHouse.Equals(pokerHand);
        }

        public static bool operator !=(FullHouse fullHouse, IPokerHand pokerHand)
        {
            if (fullHouse is null)
            {
                return pokerHand is null ? false : true;
            }
            return !fullHouse.Equals(pokerHand);
        }

        public static bool operator >(FullHouse fullHouse, IPokerHand pokerHand)
        {
            return fullHouse.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(FullHouse fullHouse, IPokerHand pokerHand)
        {
            var res = fullHouse.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(FullHouse fullHouse, IPokerHand pokerHand)
        {
            return fullHouse.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(FullHouse fullHouse, IPokerHand pokerHand)
        {
            var res = fullHouse.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
