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

        IEnumerable<Card> Triple { get; } 
        IEnumerable<Card> Kickers { get; }

        ThreeOfAkind(IEnumerable<Card> tripleCards, IEnumerable<Card> kickers)
        {
            var tripleCount = tripleCards.Count();
            if (tripleCount != 3)
            {
                var msg = $"Parameter 'triple' should contain 3 elements, containts {tripleCount} elements";
                throw new ArgumentException(msg);
            }

            var kickersCount = kickers.Count();
            if (kickersCount != 2)
            {
                var msg = $"Parameter 'kickers' should contain 2 elements, containts {kickersCount} elements";
                throw new ArgumentException(msg);
            }
            Triple = tripleCards;
            Kickers = kickers;
            Cards = tripleCards.Concat(kickers).ToArray();
        }

        public static ThreeOfAkind CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);
            throw new NotImplementedException();
        }

        internal static ThreeOfAkind CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);
            var allGroups = cards.GroupBy(c => c.Rank);
            var sizeThreeGroup = allGroups.Where(g => g.Count() == 3);

            if (sizeThreeGroup.Count() != 1) return null;

            var tripleGroup = sizeThreeGroup.Single();

            if (tripleGroup.Count() != 3) return null;

            var kickers = allGroups.Where(g => g.Count() == 1).SelectMany(g => g);

            if (!kickers.Any()) return null;

            return new ThreeOfAkind(tripleGroup.ToArray(), kickers);
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
