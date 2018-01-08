﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;

namespace TexasHoldem.Hands
{
    abstract class BaseHand: IPokerHand
    {
        public virtual HandRanks HandRank { get;  }

        public virtual IEnumerable<Card> Cards { get;  }

        public virtual int CompareTo(IPokerHand other)
        {
            throw new NotImplementedException();
        }


        #region Override Object methods

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var pokerHand = obj as IPokerHand;
            if (pokerHand == null) return false;

            return Equals(pokerHand);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + (int)this.HandRank;
            foreach (var card in Cards)
            {
                hash = hash * 23 + card.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            var cardString = string.Join(" ", (Cards.OrderByDescending(c => c.Rank)));
            return $"{HandRank}: {cardString}";
        }

        #endregion

        public virtual bool Equals(IPokerHand other)
        {
            if (other == null) return false;
            if (other.HandRank == HandRank)
            {
                return Cards.OrderBy(c => c.Rank).SequenceEqual(other.Cards.OrderBy(c => c.Rank));
            }
            return false;
        }

    }
}