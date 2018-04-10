using Game.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class TournamentRepo
    {
        public static void AddTournament(Tournament tournament)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.Add(tournament);
                _context.SaveChanges();
            }
        }
        
        public static void AddTournaments(List<Tournament> tourList)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.AddRange(tourList);
                _context.SaveChanges();
            }
        }

        public static List<Tournament> GetAllTournaments()
        {
            using (var _context = new GameContext())
            {
                var tournaments = _context.Tournaments
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                    .ToList();

                return tournaments;
            }
        }

        public static Tournament FindTournamentByID(int id)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.Where(t => t.Id == id)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                    .ToList()
                    .FirstOrDefault(); ;
                return tournament;
            }
        }

        public static Tournament FindFirstTournamentByName(string name)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.Where(t => t.Name.StartsWith(name))
                .Include(t => t.Matches)
                    .ThenInclude(m => m.Players)
                .ToList()
                .FirstOrDefault();

                return tournament;
            }
        }
        
        public static void UpdateTournamentPrize(int id, double prizeMoney)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.FirstOrDefault(t => t.Id == id);
                tournament.PrizeMoney = prizeMoney;
                _context.Tournaments.Update(tournament);
                _context.SaveChanges();
            }
        }

        public static void DeleteTournament(Tournament tour)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.Remove(tour);
                _context.SaveChanges();
            }
        }

        public static void DeleteManyTournaments(List<Tournament> tournaments)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.RemoveRange(tournaments);
                _context.SaveChanges();
            }
        }
    }
}