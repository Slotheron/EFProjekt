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
        
        public void AddTournament(Tournament tournament)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.Add(tournament);
                _context.SaveChanges();
            }
        }

        public void AddTournament(Tournament tournament, List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                foreach(Match m in matches)
                {
                    //if tournamentId is not assigned, assigns it and updates the match.
                    if (m.TournamentId == 0)
                    {
                        m.TournamentId = tournament.Id;
                        _context.Matches.Update(m);
                    }
                    //if tournamentId already exists / is assigned, display which match and to which tournament is bound.
                    else
                    {
                        var duplicate = _context.Tournaments.FirstOrDefault(t => t.Id == m.TournamentId);
                        Console.WriteLine("Match #" + m.Id + " is already assigned to Tournament - ID: " + duplicate.Id + " Name: " + duplicate.Name);
                    }
                }
                _context.Tournaments.Add(tournament);
                _context.SaveChanges();
            }
        }

        public  void AddTournaments(List<Tournament> tourList)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.AddRange(tourList);
                _context.SaveChanges();
            }
        }

        public void AddMatchesToTournament(int id, List<Match> matches)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.FirstOrDefault(t => t.Id == id);
                foreach (Match m in matches)
                {
                    //if tournamentId is not assigned, assigns it and updates the match.
                    if (m.TournamentId == 0)
                    {
                        m.TournamentId = id;
                        _context.Matches.Update(m);
                    }
                    else
                    {
                        var duplicate = _context.Tournaments.FirstOrDefault(t => t.Id == m.TournamentId);
                        Console.WriteLine("Match #" + m.Id + " is already assigned to Tournament - ID: " + duplicate.Id + " Name: " + duplicate.Name);
                    }
                }
                _context.Tournaments.Add(tournament);
                _context.SaveChanges();
            }
        }

        public List<Tournament> GetAllTournaments()
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

        public Tournament FindTournamentByID(int id)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.Where(t => t.Id == id)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                    .FirstOrDefault();

                return tournament;
            }
        }

        public Tournament FindFirstTournamentByName(string name)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.Where(t => t.Name.StartsWith(name))
                .Include(t => t.Matches)
                    .ThenInclude(m => m.Players)
                .FirstOrDefault();

                return tournament;
            }
        }
        
        public void UpdateTournamentPrize(int id, double prizeMoney)
        {
            using (var _context = new GameContext())
            {
                var tournament = _context.Tournaments.FirstOrDefault(t => t.Id == id);
                tournament.PrizeMoney = prizeMoney;
                _context.Tournaments.Update(tournament);
                _context.SaveChanges();
            }
        }

        public void DeleteTournament(Tournament tour)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.Remove(tour);
                _context.SaveChanges();
            }
        }

        public void DeleteManyTournaments(List<Tournament> tournaments)
        {
            using (var _context = new GameContext())
            {
                _context.Tournaments.RemoveRange(tournaments);
                _context.SaveChanges();
            }
        }

        //Async
        ////Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<Tournament> FindTournamentByIDAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var tournament = await _context.Tournaments.Where(t => t.Id == id)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                    .FirstOrDefaultAsync();

                return tournament;
            }
        }

        public async Task<Tournament> FindFirstTournamentByNameAsync(string name)
        {
            using (var _context = new GameContext()) 
            {
                var tournament = await _context.Tournaments.Where(t => t.Name.StartsWith(name))
                .Include(t => t.Matches)
                    .ThenInclude(m => m.Players)
                .FirstOrDefaultAsync();

                return tournament;
            }
        }

        public async Task<List<Tournament>> GetAllTournamentsAsync()
        {
            using (var _context = new GameContext())
            {
                var tournaments = await _context.Tournaments
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                    .ToListAsync();

                return tournaments;
            }
        }
    }
}