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
    class OnePair : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.OnePair; } }

        #endregion

        IEnumerable<Card> Pair { get; }
        IEnumerable<Card> Kickers { get; }

        OnePair(IEnumerable<Card> pairCards, IEnumerable<Card> kickerCards)
        {
            var pairCount = pairCards.Count();
            if (pairCount != 2)
            {
                var msg = $"Parameter 'pair' should contain 3 elements, containts {pairCount} elements";
                throw new ArgumentException(msg);
            }

            var kickersCount = kickerCards.Count();
            if (kickersCount != 3)
            {
                var msg = $"Parameter 'kickers' should contain 3 elements, containts {kickersCount} elements";
                throw new ArgumentException(msg);
            }

            Pair = pairCards;
            Kickers = kickerCards;
            Cards = Pair.Concat(Kickers).ToArray();
        }

        #region CreateInstance

        public static OnePair CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static OnePair CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);

            if (Utils.ConsequtiveCardsNoDuplicates(cards)) return null;

            if (Utils.AllSameSuit(cards)) return null;

            var rankGroups = cards.GroupBy(c => c.Rank);
            if (rankGroups.Any(g => g.Count() > 2)) return null;

            var pairGroups = rankGroups.Where(g => g.Count() == 2);

            if (pairGroups.Count() != 1) return null;

            var pair = pairGroups.Single().ToArray();
            var kickers = rankGroups.Where(g => g.Count() == 1).SelectMany(g => g).ToArray();
            return new OnePair(pair, kickers);
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

        public static bool operator ==(OnePair onePair, IPokerHand pokerHand)
        {
            if (onePair is null)
            {
                return pokerHand is null ? true : false;
            }
            return onePair.Equals(pokerHand);
        }

        public static bool operator !=(OnePair onePair, IPokerHand pokerHand)
        {
            if (onePair is null)
            {
                return pokerHand is null ? false : true;
            }
            return !onePair.Equals(pokerHand);
        }

        public static bool operator >(OnePair onePair, IPokerHand pokerHand)
        {
            return onePair.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(OnePair onePair, IPokerHand pokerHand)
        {
            var res = onePair.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(OnePair onePair, IPokerHand pokerHand)
        {
            return onePair.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(OnePair onePair, IPokerHand pokerHand)
        {
            var res = onePair.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion

    }
}
