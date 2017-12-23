using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Deck;
using System;
namespace TexasHoldem.Utilities
{
    static class Utils
    {
        public static IEnumerable<Card> ParseCards(string cards)
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

        public static void Validate(IEnumerable<Card> communityCards, IEnumerable<Card> holeCards)
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
    }
}
