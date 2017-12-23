using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Deck;

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
    }
}
