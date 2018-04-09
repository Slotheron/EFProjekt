using Game.Data;
using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class MatchModification
    {
        private static GameContext _context = new GameContext();
        
        //preferably after players have been added.
        public static void AddMatch()
        {
            Match newMatch = new Match();
            newMatch.MaxRounds = 10;
            newMatch.TournamentId = 8;
            newMatch.Time = new DateTime(2018, 4, 9, 9, 15, 0);

            _context.Matches.Add(newMatch);
            _context.SaveChanges();
            Console.WriteLine("Match with Id #" + newMatch.Id +" is scheduled for: " + newMatch.Time + " and has been added to database.");
        }

        //in future add console interface for user input
        public static void AddMatches()
        {
            Match newMatch1 = new Match();
            newMatch1.MaxRounds = 5;
            newMatch1.TournamentId = 8;
            newMatch1.Time = new DateTime(2018, 4, 9, 9, 30, 0);

            Match newMatch2 = new Match();
            newMatch2.MaxRounds = 5;
            newMatch2.TournamentId = 8;
            newMatch2.Time = new DateTime(2018, 4, 9, 9, 45, 0);

            List<Match> MatchList = new List<Match> { newMatch1, newMatch2 };
            _context.Matches.AddRange(MatchList);
            _context.SaveChanges();
            foreach(Match m in MatchList)
            {
                Console.WriteLine("Match with Id #" + m.Id + " is scheduled for: " + m.Time + " and has been added to database.");
            }
        }

        public static void GetAllMatches()
        {
            var matches = _context.Matches.ToList();

            foreach (var m in matches)
            {
                Console.WriteLine("Match #" + m.Id + " is scheduled for: " + m.Time.TimeOfDay);
            }
        }

        public static void FindMatch()
        {
            //Find by match ID
            var match1 = _context.Matches.FirstOrDefault(m => m.Id == 3);
            var match2 = _context.Matches.Find(2);
            //Find By Tournament ID
            var match3 = _context.Matches.FirstOrDefault(m => m.TournamentId == 8);
            Console.WriteLine("Match #" + match1.Id + " : " + match1.Time.TimeOfDay);
            Console.WriteLine("Match #" + match2.Id + " starts at: " + match2.Time.TimeOfDay);
        }

        public static void FindFirstMatchTime()
        {
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            int tourID = 8;
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tourID);
            Console.WriteLine("Match that starts at " + MatchTime.TimeOfDay + " is Match #" + match1.Id);
        }

        //possibility for multithreading here
        public static void UpdateMatch()
        {
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            int tourID = 8;
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tourID);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == tourID);
            _context.Matches.Update(match1);
            _context.SaveChanges();
            Console.WriteLine(tournament1.Name + " match #" + match1.Id  + " has been updated in the database");
        }

        public static void UpdateMatchDisconnected()
        {
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            int tourID = 8;
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tourID);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == tourID);
            var newContext = new GameContext();
            newContext.Matches.Update(match1);
            newContext.SaveChanges();
            Console.WriteLine(tournament1.Name + " match #" + match1.Id + " has been updated in the database");
        }

        public static void DeleteMatch()
        {
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            int tourID = 8;
            //Find By ID
            var match = _context.Matches.Find(1);
            //Find By Time / TournamentID 
            var match1 = _context.Matches.FirstOrDefault(m => m.TournamentId == tourID && m.Time == MatchTime);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == tourID);
            _context.Matches.Remove(match);
            _context.SaveChanges();
            Console.WriteLine("Match #" + match1.Id + "has been deleted from " + tournament1 + ".");
        }

        public static void DeleteManyMatchesFromTournament()
        {
            string tourName = "Dreamhack 2045";
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Name == tourName);
            var matches = _context.Matches.Where(m => m.TournamentId == tournament1.Id).ToList();

            _context.Matches.RemoveRange(matches);
            _context.SaveChanges();

            foreach(Match m in matches)
            {
                Console.WriteLine("\tMatch #" + m.Id + " has been deleted from " + tournament1.Name + ".");
            }
        }

        public static void DeleteManyMatchesDisconnected()
        {
            string tourName = "Dreamhack 2045";
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Name == tourName);
            var matches = _context.Matches.Where(m => m.TournamentId == tournament1.Id).ToList();

            var newContext = new GameContext();
            newContext.Matches.RemoveRange(matches);
            newContext.SaveChanges();

            foreach (Match m in matches)
            {
                Console.WriteLine("\tMatch #" + m.Id + " has been deleted from " + tournament1.Name + ".");
            }
        }
    }
}
