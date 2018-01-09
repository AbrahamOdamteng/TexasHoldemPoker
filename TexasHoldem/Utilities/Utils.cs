using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Deck;
using System;
namespace TexasHoldem.Utilities
{
    static class Utils
    {
        internal static IEnumerable<Card> ParseCards(string cards)
        {
            cards = cards.Trim();
            if (cards.Length == 0) return new Card[0];

            var pokerHandArray = cards.Split(' ').Where(s => s.Length > 0).ToArray();

            var cardArray = new Card[pokerHandArray.Length];
            for (int i = 0; i < pokerHandArray.Length; i++)
            {
                var card = new Card(pokerHandArray[i]);
                cardArray[i] = card;
            }
            return cardArray;
        }

        internal static void Validate(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
        {
            var communityCardCount = communityCards.Count();
            if (communityCardCount < 3 || communityCardCount > 5)
            {
                var msg = $"The number of community cards must be between 3 and 5 inclusive, {communityCardCount} where provided";
                throw new ArgumentException(msg);
            }

            var holeCardCount = holeCards.Count();
            if (holeCardCount != 2)
            {
                var msg = $"The number of hole cards must be 2, {holeCardCount} where provided";
                throw new ArgumentException(msg);
            }

            var allCards = new List<Card>(communityCards);

            var duplicateCards = allCards.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key);

            if (duplicateCards.Any())
            {
                throw new ArgumentException("Duplicate Cards are not allowed");
            }
        }

        internal static IEnumerable<string> GenerateRandomCards(int number)
        {
            var rankRandom = new Random(Guid.NewGuid().GetHashCode());
            var suitRandom = new Random(Guid.NewGuid().GetHashCode());

            var ranks = Enum.GetValues(typeof(CardRanks));
            var suits = Enum.GetValues(typeof(CardSuits));

            var result = new string[number];
            for (int i = 0; i < number; i++)
            {
                var nextRank = (CardRanks) ranks.GetValue(rankRandom.Next(ranks.Length));
                var nextSuit = (CardSuits) suits.GetValue(suitRandom.Next(suits.Length));


                string card = nextRank.ToString() + nextSuit.ToString();
                if (card.Length == 3) card = card.Substring(1);

                result[i] = card;
            }
            return result;

        }


        internal static IEnumerable<Card> GetHighestStraight(IEnumerable<Card> cards)
        {
            var cardCount = cards.Count();
            if(cardCount < 5 || cardCount > 7)
            {
                throw new ArgumentException("The number of cards provided must be between 5 and 7 inclusive");
            }

            List<Card> _noDuplicateRanks = new List<Card>();
            foreach (var myCard in cards)
            {
                if(_noDuplicateRanks.Any(c => c.Rank == myCard.Rank))
                {
                    continue;
                }

                _noDuplicateRanks.Add(myCard);
            }

            cards = _noDuplicateRanks;

            var orderedCards = cards.OrderByDescending(c => c).ToArray();
            for (int i = 0; i <= cards.Count() - 5; i++)
            {
                var fiveCards = orderedCards.Skip(i).Take(5).ToArray();
                if (IsStraight(fiveCards)) return fiveCards;
            }



            //Test Fow Low-Ace Straight
            if (cards.Any(c => c.Rank == CardRanks.A) && cards.Any(c => c.Rank == CardRanks._2))
            {
                var ascendingNoAceCards = cards.Where(c => c.Rank != CardRanks.A).OrderBy(c => c);
                var ace = cards.FirstOrDefault(c => c.Rank == CardRanks.A);

                var fiveCards = Enumerable.Concat(new[] { ace }, ascendingNoAceCards).Take(5);
                if (IsStraight(fiveCards)) return fiveCards;
            }

            return Enumerable.Empty<Card>();
        }

        internal static bool IsStraight(IEnumerable<Card> cards)
        {
            if (cards.Count() != 5)
            {
                throw new ArgumentException("The number of cards provided must be 5");
            }

            //All cards must have unique rank to qualify as a straight.
            if (cards.Select(c => c.Rank).Distinct().Count() != cards.Count()) return false;

            Func<IEnumerable<Card>, bool> IsStraightHelper = (IEnumerable<Card> myCards) => 
            {
                return !myCards.OrderBy(c => c.Rank).Select((c, i) => c.Rank - i).Distinct().Skip(1).Any();
            };


            if (cards.Any(c => c.Rank == CardRanks.A))
            {
                var isStraight = IsStraightHelper(cards.Where(c => c.Rank != CardRanks.A));
                return isStraight && cards.Any(c => c.Rank == CardRanks._2 || c.Rank == CardRanks.K);
            }
            else
            {
                return IsStraightHelper(cards);
            }
        }

        internal static Card GetHighestCard(IEnumerable<Card> cards)
        {
            var cardCount = cards.Count();
            if (cards.Count() != 5)
            {
                throw new ArgumentException("The number of cards provided must be 5");
            }

            if(cards.Any(c => c.Rank == CardRanks.A) && cards.Any(c => c.Rank == CardRanks._2))
            {
                return cards.Where(c => c.Rank != CardRanks.A).OrderBy(c => c.Rank).Last();
            }

            return cards.OrderBy(c => c.Rank).Last();
        }
    }
}
