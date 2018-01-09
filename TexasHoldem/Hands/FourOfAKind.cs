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
    class FourOfAKind : BaseHand
    {
        public override IEnumerable<Card> Cards { get; }
        public override HandRanks HandRank => HandRanks.FourOfAKind;

        public IEnumerable<Card> Quad { get; }
        public CardRanks QuadRank { get; }
        public Card Kicker { get; set; }

        private FourOfAKind(IEnumerable<Card> quad, Card kicker)
        {

            Quad = quad;
            QuadRank = quad.First().Rank;
            Kicker = kicker;
            Cards = quad.Concat(new[] { kicker }).ToArray();
            var count = Cards.Count();
            if(count != 5)
            {
                throw new ArgumentException($"A FourOfAKind hand consists of 5 cards, {count} given");
            }
        }

        public static FourOfAKind CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            if (!(GetHighestFourOfAKind(communityCards) is null))
            {
                return null;
            }

            var allCards = communityCards.Concat(holeCards);
            var fourOfAKindTuple = GetHighestFourOfAKind(allCards);
            if (fourOfAKindTuple is null)
            {
                return null;
            }
            return new FourOfAKind(fourOfAKindTuple.Item1, fourOfAKindTuple.Item2);
            
        }

        public static FourOfAKind CreateInstance(IEnumerable<Card> cards)
        {
            var fourOfAKindtuple = GetHighestFourOfAKind(cards);

            if (fourOfAKindtuple is null)
            {
                return null;
            }
            return new FourOfAKind(fourOfAKindtuple.Item1, fourOfAKindtuple.Item2);
        }

        static Tuple<IEnumerable<Card>, Card> GetHighestFourOfAKind(IEnumerable<Card> cards)
        {
            var quad = cards.GroupBy(c => c.Rank).SingleOrDefault(g => g.Count() == 4);

            if (quad == null) return null;

            var highestKicker = cards.Where(c => !quad.Contains(c)).OrderBy(c => c.Rank).Last();

            return new Tuple<IEnumerable<Card>, Card>(quad, highestKicker);
        }


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
            if (other is null) return -1;
            if (HandRank > other.HandRank) return 1;
            if (HandRank < other.HandRank) return -1;

            var otherFourOfAKind = other as FourOfAKind;
            if (otherFourOfAKind is null)
            {
                throw new ArgumentException("could not covert object to FourOfAKind");
            }

            if (QuadRank > otherFourOfAKind.QuadRank) return 1;
            if (QuadRank < otherFourOfAKind.QuadRank) return -1;

            if (Kicker.Rank > otherFourOfAKind.Kicker.Rank) return 1;
            if (Kicker.Rank < otherFourOfAKind.Kicker.Rank) return -1;

            return 0;
        }
        #endregion



        #region operator overloading

        public static bool operator ==(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            if (fourOfAKind is null)
            {
                return pokerHand is null ? true : false;
            }
            return fourOfAKind.Equals(pokerHand);
        }

        public static bool operator !=(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            if (fourOfAKind is null)
            {
                return pokerHand is null ? false : true;
            }
            return !fourOfAKind.Equals(pokerHand);
        }

        public static bool operator >(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            return fourOfAKind.CompareTo(pokerHand) == 1;
        }

        public static bool operator >=(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            var res = fourOfAKind.CompareTo(pokerHand);
            return res == 0 || res == 1;
        }

        public static bool operator <(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            return fourOfAKind.CompareTo(pokerHand) == -1;
        }

        public static bool operator <=(FourOfAKind fourOfAKind, IPokerHand pokerHand)
        {
            var res = fourOfAKind.CompareTo(pokerHand);
            return res == 0 || res == -1;
        }
        #endregion
    }
}
