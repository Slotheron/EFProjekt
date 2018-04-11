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

        public void AddMatch(Match match)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.Add(match);
                _context.SaveChanges();
            }
        }
        
        public void AddMatch(Match match, Player player1, Player player2)
        {
            using (var _context = new GameContext())
            {
                _context.PlayerMatch.Add(new PlayerMatch { PlayerId = player1.Id, MatchId = match.Id });
                _context.PlayerMatch.Add(new PlayerMatch { PlayerId = player2.Id, MatchId = match.Id });
                _context.SaveChanges();
            }
        }

        public void AddPlayerToMatch(Match match, Player player)
        {
            using (var _context = new GameContext())
            {
                _context.PlayerMatch.Add(new PlayerMatch { PlayerId = player.Id, MatchId = match.Id });
                _context.SaveChanges();
            }
        }

        public void AddPlayerToMatchById(int iDmatch, int iDplayer)
        {
            using (var _context = new GameContext())
            {
                _context.PlayerMatch.Add(new PlayerMatch { PlayerId = iDplayer, MatchId = iDmatch });
                _context.SaveChanges();
            }
        }

        public void AddMatches(List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.AddRange(matches);
                _context.SaveChanges();
            }
        }

        public List<Match> GetAllMatches()
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

        public Match FindMatchById(int id)
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
                    .FirstOrDefault();
                return match;
            }
        }

        public Match FindFirstMatchByTournamentId(int id)
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
        
        public void UpdateMatchTime(Match match, DateTime time)
        {
            using (var _context = new GameContext())
            {
                match.Time = time;
                _context.Matches.Update(match);
                _context.SaveChanges();
            }
        }

        public void DeleteMatch(Match match)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.Remove(match);
                _context.SaveChanges();
            }
        }

        public void DeleteManyMatchesFromTournament(List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                _context.Matches.RemoveRange(matches);
                _context.SaveChanges();
            }
        }

        //Async
        //Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<Match> GetMatchByIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var match = await _context.Matches.Where(m => m.Id == id)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .FirstOrDefaultAsync();
                return match;
            }
        }

        public async Task<Match> FindFirstMatchByTournamentIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var match = await _context.Matches.Where(m => m.TournamentId == id)
                    .OrderByDescending(m => m.Time)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .FirstOrDefaultAsync();
                return match;
            }
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            using (var _context = new GameContext())
            {
                var matches = await _context.Matches
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(m => m.Players)
                        .ThenInclude(pm => pm.Player)
                        .ThenInclude(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .ToListAsync();
                return matches;
            }
        }
    }
}
