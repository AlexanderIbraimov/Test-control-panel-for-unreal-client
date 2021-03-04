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
        
        //Baccarat and Poker
        PlayerCard = 200,
        DealerCard = 201,

        //Roulette
        ThrowTheBall = 100
    }
}
