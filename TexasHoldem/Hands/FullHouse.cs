﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
using TexasHoldem.Utilities;
namespace TexasHoldem.Hands
{
    class FullHouse : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.FullHouse; } }

        #endregion


        IEnumerable<Card> Triple { get; set; }
        IEnumerable<Card> Pair { get; set; }

        FullHouse(IEnumerable<Card> tripleCards, IEnumerable<Card> pairCards)
        {
            var tripleCount = tripleCards.Count();
            if (tripleCount != 3)
            {
                var msg = $"Parameter 'triple' should contain 3 elements, containts {tripleCount} elements";
                throw new ArgumentException(msg);
            }

            var pairCount = pairCards.Count();
            if (pairCount != 2)
            {
                var msg = $"Parameter 'pair' should contain 2 elements, containts {pairCount} elements";
                throw new ArgumentException(msg);
            }

            Triple = tripleCards;
            Pair = pairCards;
            Cards = tripleCards.Concat(pairCards).ToArray();
        }

        #region CreateInstance

        public static FullHouse CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static FullHouse CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);

            var rankGroups = cards.GroupBy(c => c.Rank);

            var tripleGroup = rankGroups.Where(g => g.Count() == 3);
            if (!tripleGroup.Any()) return null;

            var pairGroup = rankGroups.Where(g => g.Count() == 2);
            if (!pairGroup.Any()) return null;


            var triple = tripleGroup.Single();
            var pair = pairGroup.Single();
            return new FullHouse(triple, pair);
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

            var otherHand = (FullHouse)ConvertToThisType(other);
            var cmp = Utils.ComapreCards(Triple, otherHand.Triple);

            if (cmp != 0) return cmp;

            return Utils.ComapreCards(Pair, otherHand.Pair);
        }
        #endregion
    }
}
