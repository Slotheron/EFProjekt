﻿using Game.Data;
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
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));

            Match newMatch = new Match();
            newMatch.MaxRounds = 10;
            newMatch.TournamentId = tour.Id;
            newMatch.Time = new DateTime(2018, 4, 9, 9, 15, 0);

            _context.Matches.Add(newMatch);
            _context.SaveChanges();
            Console.WriteLine("\nMatch with Id #" + newMatch.Id +" is scheduled for: " + newMatch.Time + " and has been added to database.");
        }

        //in future add console interface for user input
        public static void AddMatches()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));

            Match newMatch1 = new Match();
            newMatch1.MaxRounds = 5;
            newMatch1.TournamentId = tour.Id;
            newMatch1.Time = new DateTime(2018, 4, 9, 9, 30, 0);

            Match newMatch2 = new Match();
            newMatch2.MaxRounds = 5;
            newMatch2.TournamentId = tour.Id;
            newMatch2.Time = new DateTime(2018, 4, 9, 9, 45, 0);

            List<Match> MatchList = new List<Match> { newMatch1, newMatch2 };
            _context.Matches.AddRange(MatchList);
            _context.SaveChanges();
            foreach(Match m in MatchList)
            {
                Console.WriteLine("\nMatch with Id #" + m.Id + " is scheduled for: " + m.Time + " and has been added to database.");
            }
        }

        public static void GetAllMatches()
        {
            var matches = _context.Matches.ToList();

            foreach (var m in matches)
            {
                Console.WriteLine("\nMatch #" + m.Id + " is scheduled for: " + m.Time.TimeOfDay);
            }
        }

        public static void FindMatch()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));

            var match1 = _context.Matches.FirstOrDefault(m => m.TournamentId == tour.Id);
            var match2 = _context.Matches.LastOrDefault(m => m.TournamentId == tour.Id);
            //Find By Tournament ID
            if (match1.Id != match2.Id)
            {
                Console.WriteLine("\nMatch #" + match1.Id + " : " + match1.Time.TimeOfDay);
                Console.WriteLine("\nMatch #" + match2.Id + " starts at: " + match2.Time.TimeOfDay);
            }
            else
            {
                Console.WriteLine("\nMatch #" + match1.Id + " : " + match1.Time.TimeOfDay);
                Console.WriteLine("\nDuplicate Match");
            }
        }

        public static void FindFirstMatchTime()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tour.Id);
            Console.WriteLine("\nMatch that starts at " + MatchTime.TimeOfDay + " is Match #" + match1.Id);
        }

        //possibility for multithreading here
        public static void UpdateMatch()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tour.Id);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == tour.Id);
            _context.Matches.Update(match1);
            _context.SaveChanges();
            Console.WriteLine("\n" + tournament1.Name + " match #" + match1.Id  + " has been updated in the database");
        }

        public static void UpdateMatchDisconnected()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            //Find By Time
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By Tournament ID
            var match1 = _context.Matches.FirstOrDefault(m => m.Time == MatchTime && m.TournamentId == tour.Id);
            var tournament1 = _context.Tournaments.FirstOrDefault(t => t.Id == tour.Id);
            var newContext = new GameContext();
            newContext.Matches.Update(match1);
            newContext.SaveChanges();
            Console.WriteLine("\n" + tournament1.Name + " match #" + match1.Id + " has been updated in the database");
        }

        public static void DeleteMatch()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            var MatchTime = new DateTime(2018, 4, 9, 9, 15, 0);
            //Find By ID
            //var match = _context.Matches.Find(4);
            //Find By Time / TournamentID 
            var match1 = _context.Matches.FirstOrDefault(m => m.TournamentId == tour.Id && m.Time == MatchTime);
            _context.Matches.Remove(match1);
            _context.SaveChanges();
            Console.WriteLine("\nMatch #" + match1.Id + "has been deleted from " + tour.Name + ".");
        }

        public static void DeleteManyMatchesFromTournament()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            var matches = _context.Matches.Where(m => m.TournamentId == tour.Id).ToList();

            _context.Matches.RemoveRange(matches);
            _context.SaveChanges();

            foreach(Match m in matches)
            {
                Console.WriteLine("\nMatch #" + m.Id + " has been deleted from " + tour.Name + ".");
            }
        }

        public static void DeleteManyMatchesDisconnected()
        {
            Tournament tour = _context.Tournaments.FirstOrDefault(t => t.Name.StartsWith("Dreamhack"));
            var matches = _context.Matches.Where(m => m.TournamentId == tour.Id).ToList();

            var newContext = new GameContext();
            newContext.Matches.RemoveRange(matches);
            newContext.SaveChanges();

            foreach (Match m in matches)
            {
                Console.WriteLine("\nMatch #" + m.Id + " has been deleted from " + tour.Name + ".");
            }
        }
    }
}
