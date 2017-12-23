using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Hands;
using TexasHoldem.Deck;

namespace TexasHoldem.Interfaces
{
    interface IPokerHand: IComparable<IPokerHand>, IEquatable<IPokerHand>
    {
        HandRanks HandRank { get; }
        IEnumerable<Card> Cards { get; }
    }
}
