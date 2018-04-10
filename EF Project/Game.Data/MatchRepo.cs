using Game.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class MatchRepo
    {
        public static void AddMatch(Match match)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.Add(match);
                _context.SaveChanges();
            }
        }
        
        public static void AddMatches(List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.AddRange(matches);
                _context.SaveChanges();
            }
        }

        public static List<Match> GetAllMatches()
        {
            using (var _context = new GameContext())
            {
                var matches = _context.Matches
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player )
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .ToList();
                return matches;
            }
        }

        public static Match FindMatchById(int id)
        {
            using (var _context = new GameContext())
            {
                var match = _context.Matches.Where(m => m.Id == id)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .ToList()
                    .FirstOrDefault();
                return match;
            }
        }

        public static Match FindFirstMatchByTournamentId(int id)
        {
            using (var _context = new GameContext())
            {
                var match = _context.Matches.Where(m => m.TournamentId == id)
                    .OrderByDescending(m => m.Time)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .FirstOrDefault();
                return match;
            }
        }
        
        public static void UpdateMatch(Match match, DateTime time)
        {
            using (var _context = new GameContext())
            {
                match.Time = time;
                _context.Matches.Update(match);
                _context.SaveChanges();
            }
        }

        public static void DeleteMatch(Match match)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.Remove(match);
                _context.SaveChanges();
            }
        }

        public static void DeleteManyMatchesFromTournament(List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.RemoveRange(matches);
                _context.SaveChanges();
            }
        }
    }
}
