using Game.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class PlayerRepo
    {
        public void AddPlayer(Player player)
        {
            using (var _context = new GameContext())
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }
        }

        public void AddPlayer(Player player, Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Players.Add(player);
                _context.PlayerCharacter.Add(new PlayerCharacter{ Player = player, Character = character });
                _context.SaveChanges();
            }
        }
        
        public void AddCharacterToPlayer(Player player, Character character)
        {
            using (var _context = new GameContext())
            {
                _context.PlayerCharacter.Add(new PlayerCharacter { PlayerId = player.Id, CharacterId = character.Id });
                _context.SaveChanges();
            }
        }

        public void AddPlayers(List<Player> players)
        {
            using (var _context = new GameContext())
            {
                _context.Players.AddRange(players);
                _context.SaveChanges();
            }
        }
        
        public Player GetPlayerById(int id)
        {
            using (var _context = new GameContext())
            {
                var player = _context.Players.Where(p => p.Id == id)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character)
                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();
                return player;
            }
        }

        public Player GetPlayerByName(string name)
        {
            using (var _context = new GameContext())
            {
                var player = _context.Players.Where(p => p.Name == name)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character)
                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();
                return player;
            }
        }

        public List<Player> GetAllPlayers()
        {
            using (var _context = new GameContext())
            {
                var players = _context.Players
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character.Moves)
                    .ToList();
                return players;
            }
        }

        public List<Player> GetAllPlayersByCharacterId(int charID)
        {
            using (var _context = new GameContext())
            {
                var playerChars = _context.PlayerCharacter.Where(pc => pc.CharacterId == charID).ToList();
                var players = new List<Player>();
                foreach(PlayerCharacter pc in playerChars)
                {
                    if (pc.Character.Id == charID)
                    {
                        if (!players.Contains(pc.Player))
                        {
                            players.Add(pc.Player);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                return players; 
            }
        }

        public List<Player> GetAllPlayersByCharacterName(string name)
        {
            using (var _context = new GameContext())
            {
                var playerChars = _context.PlayerCharacter.Where(pc => pc.Character.Name == name).ToList();
                var players = new List<Player>();
                foreach (PlayerCharacter pc in playerChars)
                {
                    players.Add(pc.Player);
                }

                return players;
            }
        }

        //each player has a list of characters, but characters does not have a list of players despite it being a many to many relationship.
        //idea behind that is a player can play multiple characters but a character can also be played by multiple people. At most players can have up to 3 characters per match but characters
        //can be played by people worldwide.
        public List<Player> GetAllPlayersByCharacterInTournament(int tournamentID, Character character) 
        {
            using (var _context = new GameContext())
            {
                var players = new List<Player>();
                var tournament = _context.Tournaments.Where(t => t.Id == tournamentID)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                            .ThenInclude(pm => pm.Player)
                                .ThenInclude(p => p.Characters)
                                    .ThenInclude(c => c.Character)
                                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach(Match m in tournament.Matches)
                {
                    foreach(PlayerMatch pm in m.Players)
                    {
                        foreach(PlayerCharacter pc in pm.Player.Characters)
                        {
                            if(pc.Character.Id == character.Id)
                            {
                                if (!players.Contains(pc.Player))
                                {
                                    players.Add(pc.Player);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                return players;
            }
        }

        public List<Player> GetAllPlayersByMatchId(int matchID)
        {
            using (var _context = new GameContext())
            {
                var players = new List<Player>();
                var match = _context.Matches.Where(m => m.Id == matchID)
                    .Include(m => m.Players)
                            .ThenInclude(pm => pm.Player)
                                .ThenInclude(p => p.Characters)
                                    .ThenInclude(c => c.Character)
                                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach(PlayerMatch pm in match.Players)
                {
                    if (!players.Contains(pm.Player))
                    {
                        players.Add(pm.Player);
                    }
                    else
                    {
                        continue;
                    }
                }
                return players;
            }
        }

        //executes the GetAllPlayersByMatchId() for each match in specified tournament and saves to a list. Handles duplicates.
        public List<Player> GetAllPlayersByTournamentId(int tournamentID)
        {
            using (var _context = new GameContext())
            {
                var players = new List<Player>();

                var tournament = _context.Tournaments.Where(t => t.Id == tournamentID)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                            .ThenInclude(pm => pm.Player)
                                .ThenInclude(p => p.Characters)
                                    .ThenInclude(c => c.Character)
                                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach (Match m in tournament.Matches)
                {
                    players.AddRange(GetAllPlayersByMatchId(m.Id));
                }
                return players;
            }
        }

        public List<Player> GetAllPlayersByTournamentName(string name)
        {
            using (var _context = new GameContext())
            {
                var players = new List<Player>();

                var tournament = _context.Tournaments.Where(t => t.Name == name)
                    .Include(t => t.Matches)
                        .ThenInclude(m => m.Players)
                            .ThenInclude(pm => pm.Player)
                                .ThenInclude(p => p.Characters)
                                    .ThenInclude(c => c.Character)
                                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach (Match m in tournament.Matches)
                {
                    players.AddRange(GetAllPlayersByMatchId(m.Id));
                }
                return players;
            }
        }

        public void UpdatePlayerName(Player player, string name)
        {
            using (var _context = new GameContext()) 
            {
                player.Name = name;
                _context.Players.Update(player);
                _context.SaveChanges();
            }
        }

        public void UpdatePlayerAge(Player player, int age)
        {
            using (var _context = new GameContext())
            {
                player.Age = age;
                _context.Players.Update(player);
                _context.SaveChanges();
            }
        }
        
        public void UpdatePlayerNationality(Player player, string nationality)
        {
            using (var _context = new GameContext())
            {
                player.Nationality = nationality;
                _context.Players.Update(player);
                _context.SaveChanges();
            }
        }

        public void DeletePlayer(Player player)
        {
            using (var _context = new GameContext())
            {
                var playerMatches = _context.PlayerMatch.Where(pm => pm.PlayerId == player.Id).ToList();
                var playerCharacters = _context.PlayerCharacter.Where(pc => pc.PlayerId == player.Id).ToList();
                _context.PlayerMatch.RemoveRange(playerMatches);
                _context.PlayerCharacter.RemoveRange(playerCharacters);
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
        }

        public void DeleteManyPlayers(List<Player> players)
        {
            using (var _context = new GameContext())
            {
                foreach(Player player in players)
                {
                    var playerMatches = _context.PlayerMatch.Where(pm => pm.PlayerId == player.Id).ToList();
                    var playerCharacters = _context.PlayerCharacter.Where(pc => pc.PlayerId == player.Id).ToList();
                    _context.PlayerMatch.RemoveRange(playerMatches);
                    _context.PlayerCharacter.RemoveRange(playerCharacters);
                }
                _context.Players.RemoveRange(players);
                _context.SaveChanges();
            }
        }

        //Async
        ////Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var player = await _context.Players.Where(p => p.Id == id)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character.Moves)
                    .FirstOrDefaultAsync();
                return player;
            }
        }

        public async Task<Player> GetPlayerByNameAsync(string name)
        {
            using (var _context = new GameContext())
            {
                var player = await _context.Players.Where(p => p.Name == name)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character.Moves)
                    .FirstOrDefaultAsync();
                return player;
            }
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            using (var _context = new GameContext())
            {
                var players = await _context.Players
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Position)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character.Moves)
                        .ToListAsync();
                return players;
            }
        }
    }
}
