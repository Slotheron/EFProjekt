using Game.Data;
using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class PlayerModification
    {
        private static GameContext _context = new GameContext();

        public static void AddPlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Name = "Joe";
            newPlayer.Age = 26;
            newPlayer.Nationality = "American";

            _context.Players.Add(newPlayer);
            _context.SaveChanges();
            Console.WriteLine("\nId:" + newPlayer.Id + "\nName: " + newPlayer.Name + " has been added to the database.");
        }

        //in future add console interface for user input
        public static void AddPlayers()
        {
            Player newPlayer1 = new Player();
            newPlayer1.Name = "Petra";
            newPlayer1.Age = 26;
            newPlayer1.Nationality = "Swedish";

            Player newPlayer2 = new Player();
            newPlayer2.Name = "ZergToss";
            newPlayer2.Age = 28;
            newPlayer2.Nationality = "Dutch";

            List<Player> PlayerList = new List<Player> { newPlayer1, newPlayer2 };
            _context.Players.AddRange(PlayerList);
            _context.SaveChanges();
            foreach (Player p in PlayerList)
            {
                Console.WriteLine("\nId:" + p.Id + "\nName: " + p.Name + " has been added to the database.");
            }
        }

        public static void GetAllPlayers()
        {
            var players = _context.Players.ToList();

            foreach (var p in players)
            {
                Console.WriteLine("\n\nName:" + p.Id + ": " + p.Name + "\nAge:" + p.Age + "\nNationality:" + p.Nationality);
            }
        }

        public static void FindPlayer()
        {
            var player1 = _context.Players.FirstOrDefault(p => p.Name == "Joe");
            var player2 = _context.Players.FirstOrDefault(p => p.Name == "Petra");
            Console.WriteLine("\nId:" + player1.Id + "\nName:" + player1.Name + "\nAge: " + player1.Age + "\nNationality: " + player1.Nationality);
            Console.WriteLine("\nId: " + player2.Id+ "\nName:" + player2.Name + "\nAge: " + player2.Age + "\nNationality: " + player2.Nationality);
        }

        //possibility for multithreading here
        public static void UpdatePlayer()
        {
            var player = _context.Players.FirstOrDefault(p => p.Name == "Joe");
            player.Nationality = "Swedish";
            _context.Players.Update(player);
            _context.SaveChanges();
            Console.WriteLine("\nId" + player.Id + "\nName: " + player.Name + "'s Nationality has been updated to " + player.Nationality + ".");
        }

        public static void UpdatePlayerDisconnected()
        { 
            var newContext = new GameContext();
            var player = _context.Players.FirstOrDefault(p => p.Name == "Joe");
            player.Nationality = "Swedish";
            newContext.Players.Update(player);
            newContext.SaveChanges();
            Console.WriteLine("\nId" + player.Id + "\nName: " + player.Name + "'s Nationality has been updated to " + player.Nationality + ".");
        }

        public static void DeletePlayer()
        {
            var player = _context.Players.FirstOrDefault(p => p.Name == "ZergToss");
            _context.Players.Remove(player);
            _context.SaveChanges();
            Console.WriteLine("\nId" + player.Id + "\nName: " + player.Name + " has been removed from the database.");
        }

        public static void DeleteManyPlayers()
        {
            var players = _context.Players.Where(p => p.Name == "Petra" || p.Name == "Joe").ToList();

            _context.Players.RemoveRange(players);
            _context.SaveChanges();

            foreach (Player p in players)
            {
                Console.WriteLine("\nId" + p.Id + "\nName: " + p.Name + " has been removed from the database.");
            }
        }

        public static void DeleteManyPlayersDisconnected()
        {
            var newContext = new GameContext();
            var players = _context.Players.Where(p => p.Name == "Petra" || p.Name == "Joe").ToList();

            newContext.Players.RemoveRange(players);
            newContext.SaveChanges();

            foreach (Player p in players)
            {
                Console.WriteLine("\nId" + p.Id + "\nName: " + p.Name + " has been removed from the database.");
            }
        }
    }
}
