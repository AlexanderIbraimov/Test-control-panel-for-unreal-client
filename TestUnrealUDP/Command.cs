using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestControlPanel
{
    public static class Command
    {
        private static ConcurrentQueue<string> commands = new ConcurrentQueue<string>();

        public static void SetGameType(string nameGame)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.SetGameType},
                {"GameType", nameGame}
            };

            AddCommand(command);
        }

        public static void AddPlayer(int id, string name)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.AddPlayer},
                {"PlayerId", id},
                {"Name", name}
            };
            AddCommand(command);
        }

        public static void RemovePlayer(int id)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.RemovePlayer},
                {"PlayerId", id}
            };

            AddCommand(command);
        }

        public static void ResetGame()
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.ResetGame}
            };
            AddCommand(command);
        }

        public static void SetStatus(string status)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.Status},
                {"Status", status}
            };
            AddCommand(command);
        }

        public static void SetPlaceBet(int playerId, decimal bets, int hand)
        {
            //var chips = GetAnArrayOfBets(bets);
            //if(chips.Length == 0 && bets!= Decimal.Zero)
            //    return;
            //var command = new Dictionary<string, object>
            //{
            //    {"MessageType", (int) MessageType.PlaceBet},
            //    {"PlayerId", playerId},
            //    {"Hand", hand},
            //    {"Bets", chips}
            //};
            //AddCommand(command);
        }


        public static void SetDealerCard(string card, int number)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.DealerCard},
                {"Card", card},
                {"Number", number}
            };
            AddCommand(command);
        }

        public static void SetPlayerCard(int playerId, string card, int number)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.PlayerCard},
                {"PlayerId", playerId},
                {"Card", card},
                {"Number", number}
            };
            AddCommand(command);
        }

        public static void DealerFlipCard(int number)
        {
            //var command = new Dictionary<string, object>
            //{
            //    {"MessageType", (int) MessageType.DealerFlipCard},
            //    {"Number", number}
            //};
            //AddCommand(command);
        }

        public static void PlayerFlipCard(int playerId, int number)
        {
            //var command = new Dictionary<string, object>
            //{
            //    {"MessageType", (int) MessageType.PlayerFlipCard},
            //    {"PlayerId", playerId},
            //    {"Number", number}
            //};
            //AddCommand(command);
        }

        //Roulette
        public static void ThrowTheBall(int result)
        {
            var command = new Dictionary<string, object>
            {
                {"MessageType", (int) MessageType.ThrowTheBall},
                {"Number", result}
            };
            AddCommand(command);
        }
        //Baccarat




        //Blackjack

        public static void Split(int playerId)
        {
            //var command = new Dictionary<string, object>
            //{
            //    {"MessageType", (int) MessageType.Split},
            //    {"PlayerId", playerId}
            //};
            //AddCommand(command);
        }

        public static void Insurance(int playerId, decimal bets, int hand = 1)
        {
            //var chips = GetAnArrayOfBets(bets);
            //if (chips.Length == 0 && bets != Decimal.Zero)
            //    return;
            //var command = new Dictionary<string, object>
            //{
            //    {"MessageType", (int) MessageType.Insurance},
            //    {"PlayerId", playerId},
            //    {"Hand", hand},
            //    {"Bets", chips}
            //};
            //AddCommand(command);
        }

        private static void AddCommand(Dictionary<string, object> command)
        {
            try
            {
                commands.Enqueue(JsonConvert.SerializeObject(command));
            }
            catch (Exception e)
            {

            }
        }

        public static void AddCommand(string command)
        {
            try
            {
                commands.Enqueue(command);
            }
            catch (Exception e)
            {

            }
        }

        public static string GetNextCommand()
        {
            if (commands.TryDequeue(out var command))
                return command;

            return null;
        }


        private static decimal[] GetAnArrayOfBets(decimal value)
        {
            if (value == decimal.Zero)
            {
                return new decimal[] { 0 };
            }

            var weights = new[] { 100M, 50M, 20M, 10M, 5M, 3M, 1M, 0.5M, 0.1M, 0.05M };
            var result = new List<decimal>();

            foreach (var weight in weights)
            {
                while (value >= weight)
                {
                    result.Add(weight);
                    value -= weight;
                }
            }
            return result.ToArray();
        }
    }
}
