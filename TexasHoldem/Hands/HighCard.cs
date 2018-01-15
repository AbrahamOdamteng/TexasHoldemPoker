using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
using TexasHoldem.Utilities;
namespace TexasHoldem.Hands
{
    class HighCard : BaseHand
    {
        #region implementation of IPokerHand Memberes

        public override IEnumerable<Card> Cards { get; }

        public override HandRanks HandRank { get { return HandRanks.HighCard; } }

        #endregion

        private HighCard(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        #region CreateInstance

        public static HighCard CreateInstance(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            Utils.Validate(communityCards, holeCards);

            var holeCardPair = !holeCards.Select(c => c.Rank).Distinct().Skip(1).Any();
            if (holeCardPair) return null;//At least one pair found

            Func<Card, bool> rankFunc = (Card inputCard) =>
            {
                return communityCards.Where(c => c.Rank == inputCard.Rank).Any();
            };

            foreach(var holeCard in holeCards)
            {
                if (rankFunc(holeCard)) return null;//At least one pair found
            }


            Func<CardSuits,int, bool>  suitFunc = (CardSuits inputSuit, int count) =>
            {
                return communityCards.Where(c => c.Suit == inputSuit).Count() >= count;
            };

            var holeCardsHaveSameSuit = !holeCards.Select(c => c.Suit).Distinct().Skip(1).Any();
            if (holeCardsHaveSameSuit)
            {
                if (suitFunc(holeCards.First().Suit, 3)) return null; //flush present
            }
            else
            {
                foreach (var holeCard in holeCards)
                {
                    if (suitFunc(holeCard.Suit,4)) return null;//At least one pair found
                }
            }

            var isFlushType = communityCards.Select(c => c.Suit).Distinct().Skip(1).Any();


            //find High-Ace straight
            var allCards = communityCards.Concat(holeCards).OrderBy(c => c.Rank).ToArray();
            for(int i =0; i < allCards.Length - BaseHand.CardsPerHand; i++)
            {
                var myCards = allCards.Skip(i).Take(BaseHand.CardsPerHand);
                if (Utils.IsStraight(myCards) && holeCards.Where(c => myCards.Contains(c)).Any())
                {
                    return null;//Straight Found
                }
            }
            //Find Low Ace Straight
            if (allCards.Any(c => c.Rank == CardRanks.A) && allCards.Any(c => c.Rank == CardRanks._2))
            {
                var candidate = new[] { allCards.First(c => c.Rank == CardRanks.A) }.Concat(allCards.Take(4));
                if (Utils.IsStraight(candidate) && holeCards.Where(c => candidate.Contains(c)).Any())
                {
                    return null;//Straight Found
                }
            }

            return new HighCard(holeCards);
        }

        internal static HighCard CreateInstance(IEnumerable<Card> cards)
        {
            Utils.Validate(cards);
            if (Utils.AllSameSuit(cards)) return null;
            if (Utils.ConsequtiveCardsNoDuplicates(cards)) return null;

            var containsDuplicateRanks = cards.GroupBy(c => c.Rank).Where(g => g.Count() > 1).Any();
            if (containsDuplicateRanks) return null;

            return new HighCard(cards);
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

            var zippedCards = Enumerable.Zip(
                Cards.OrderBy(c => c.Rank),
                other.Cards.OrderBy(c => c.Rank),
                (left, right) => new Tuple<Card, Card>(left, right));

            foreach(var zip in zippedCards)
            {
                if (zip.Item1.Rank > zip.Item2.Rank) return 1;
                if (zip.Item1.Rank < zip.Item2.Rank) return -1;
            }
            return 0;
        }
        #endregion
    }
}
