using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Deck;

namespace TexasHoldem.Game
{
    class Game
    {
        Guid Id { get; set; }
        GameStates GameState { get; set; }

        IEnumerable<Player> Players { get; set; }
        IEnumerable<Card> CommunityCards { get; set; }

        Player Dealer { get; set; }
        Player LittleBlind { get; set; }
        Player BigBlind { get; set; }

        int Pot { get; set; }

        void AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }


    }
}
