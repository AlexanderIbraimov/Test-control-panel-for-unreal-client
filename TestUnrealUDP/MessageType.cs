using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControlPanel
{
    public enum MessageType
    {
        //Common commands
        SetGameType = 0,
        AddPlayer = 1,
        RemovePlayer = 2,
        ResetGame = 3,
        Status = 4,
        PlaceBet = 5,
        DealerCard = 6,
        PlayerCard = 7,
        PlayerFlipCard = 8,
        DealerFlipCard = 9,

        //Roulette
        ThrowTheBall = 100,

        //Blackjack
        Split = 400,
        Insurance = 401
    }
}
