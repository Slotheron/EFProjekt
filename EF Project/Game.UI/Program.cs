using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Data;
using Game.Domain;

namespace Game.UI
{
    class Program
    {
        private static GameContext _context = new GameContext();

        static void Main(string[] args)
        {
            //AddTournament();
            //AddTournaments();
            //GetAllTournaments();
            //FindTournament();
            //FindFirstTournament();
            //UpdateTournament();
            //UpdateTournamentDisconnected()
            //DeleteTournament();
            //DeleteManyTournaments();
            //DeleteManyDisconnectedTournaments();
        }

        

        private static void AddTournament()
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
        private static void AddTournaments()
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

        private static void GetAllTournaments()
        {
            var tournaments1 = _context.Tournaments.ToList();
           
            foreach (var tournament in tournaments1)
            {
                Console.WriteLine(tournament.Id + ": " + tournament.Name);
            }
        }

        private static void FindTournament()
        {
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == 8);
            var tournament2 = _context.Tournaments.Find(7);
            Console.WriteLine(tournament1.Name);
            Console.WriteLine(tournament2.Name);
        }

        private static void FindFirstTournament()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith(tourName));
            Console.WriteLine(tournament1.Id + ": " + tournament1.Name);
        }

        //possibility for multithreading here
        private static void UpdateTournament()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.Where(t => t.Name.StartsWith(tourName) && t.PrizeMoney == 75000).ToList();

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

        private static void UpdateTournamentDisconnected()
        {
            string tourName = "Dream";
            var tournament1 = _context.Tournaments.Where(t => t.Name.StartsWith(tourName) && t.PrizeMoney == 75000).ToList();

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

        private static void DeleteTournament()
        {
            var movie = _context.Tournaments.Find(7);
            _context.Tournaments.Remove(movie);
            _context.SaveChanges();
        }

        private static void DeleteManyTournaments()
        {
            string tourName = "Dreamhack";
            var tournaments = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();
            _context.Tournaments.RemoveRange(tournaments);
            _context.SaveChanges();
        }

        private static void DeleteManyDisconnectedTournaments()
        {
            string tourName = "Dreamhack";
            var tournaments = _context.Tournaments.Where(t => t.Name.StartsWith(tourName)).ToList();
            
            var newContext = new GameContext();
            newContext.Tournaments.RemoveRange(tournaments);
            newContext.SaveChanges();
        }
    }
}
