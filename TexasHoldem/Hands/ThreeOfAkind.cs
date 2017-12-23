using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;
using TexasHoldem.Interfaces;
namespace TexasHoldem.Hands
{
    class ThreeOfAkind : IPokerHand
    {
        public HandRanks HandRank => throw new NotImplementedException();

        public IEnumerable<Card> Cards => throw new NotImplementedException();

        public int CompareTo(IPokerHand other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
