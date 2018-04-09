using Game.Data;
using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class TournamentModification
    {
        private static GameContext _context = new GameContext();
        public static void AddTournament()
        {
            Tournament newTour = new Tournament();
            newTour.Name = "Dreamhack 2044";
            newTour.Date = System.DateTime.Now;
            newTour.Country = "Sweden";
            newTour.PrizeMoney = 50000;

            _context.Tournaments.Add(newTour);
            _context.SaveChanges();
            Console.WriteLine(newTour.Name + " added to database.");
        }

        //in future add console interface for user input
        public static void AddTournaments()
        {
            Tournament newTour1 = new Tournament();
            newTour1.Name = "Dreamhack 2045";
            newTour1.Date = System.DateTime.Now;
            newTour1.Country = "Sweden";
            newTour1.PrizeMoney = 75000;

            Tournament newTour2 = new Tournament();
            newTour2.Name = "Dreamhack 2046";
            newTour2.Date = System.DateTime.Now;
            newTour2.Country = "Sweden";
            newTour2.PrizeMoney = 120000;

            List<Tournament> TourList = new List<Tournament> { newTour1, newTour2 };
            _context.Tournaments.AddRange(TourList);
            _context.SaveChanges();
        }

        public static void GetAllTournaments()
        {
            var tournaments1 = _context.Tournaments.ToList();

            foreach (var tournament in tournaments1)
            {
                Console.WriteLine(tournament.Id + ": " + tournament.Name);
            }
        }

        public static void FindTournament()
        {
            //var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == 8);
            //var tournament2 = _context.Tournaments.Find(14);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Name == "Dreamhack 2045");
            var tournament2 = _context.Tournaments.FirstOrDefault(t => t.Name == "Dreamhack 2046");
            Console.WriteLine(tournament1.Name);
            Console.WriteLine(tournament2.Name);
        }

        public static void FindFirstTournament()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith(tourName));
            Console.WriteLine(tournament1.Id + ": " + tournament1.Name);
        }

        //possibility for multithreading here
        public static void UpdateTournament()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();

            List<Tournament> consoleList = new List<Tournament>();
            var newContext = new GameContext();

            foreach (Tournament t in tournament1)
            {
                t.PrizeMoney = 77000;
                _context.Tournaments.Update(t);
                consoleList.Add(t);

            }
            _context.SaveChanges();

            foreach (Tournament t2 in consoleList)
            {
                Console.WriteLine(t2.Id + ": " + t2.Name + " - has been updated in the database");
            }
        }

        public static void UpdateTournamentDisconnected()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();

            List<Tournament> consoleList = new List<Tournament>();
            var newContext = new GameContext();

            foreach (Tournament t in tournament1)
            {
                t.PrizeMoney = 77000;
                newContext.Tournaments.Update(t);
                consoleList.Add(t);

            }
            newContext.SaveChanges();

            foreach (Tournament t2 in consoleList)
            {
                Console.WriteLine(t2.Id + ": " + t2.Name + " - has been updated in the database");
            }
        }

        public static void DeleteTournament()
        {
            string tourName = "Dreamhack";
            var tournament = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith(tourName));
            _context.Tournaments.Remove(tournament);
            _context.SaveChanges();
            Console.WriteLine("\n" + tournament.Id + ": " + tournament.Name + " deleted from database.");
        }

        public static void DeleteManyTournaments()
        {
            string tourName = "Dreamhack";
            var tournaments = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();
            _context.Tournaments.RemoveRange(tournaments);
            _context.SaveChanges();
            foreach(Tournament t in tournaments)
            {
                Console.WriteLine("\n" + t.Id + ": " + t.Name + " deleted from database.");
            }
        }
         
        public static void DeleteManyTournamentsDisconnected()
        {
            string tourName = "Dreamhack";
            var tournaments = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();

            var newContext = new GameContext();
            newContext.Tournaments.RemoveRange(tournaments);
            newContext.SaveChanges();
        }
    }
}
