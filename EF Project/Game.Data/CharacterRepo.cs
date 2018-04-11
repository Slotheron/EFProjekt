using Game.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class CharacterRepo
    {
        public void AddCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.Add(character);
                _context.SaveChanges();
            }
        }
        
        public void AddCharacters(List<Character> characters)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.AddRange(characters);
                _context.SaveChanges();
            }
        }
        
        public List<Character> GetAllCharacters()
        {
            using (var _context = new GameContext())
            {
                var characters = _context.Characters
                    .Include(c => c.Moves)
                    .ToList();
                return characters;
            }
        }

        public Character GetCharacterById(int id)
        {
            using (var _context = new GameContext())
            {
                var character = _context.Characters
                    .Where(c => c.Id == id)
                    .Include(c => c.Moves)
                    .FirstOrDefault();
                return character;
            }
        }

        public List<Character> GetCharacterByPlanet(string planet)
        {
            using (var _context = new GameContext())
            {
                var characters = _context.Characters.Where(c => c.Planet == planet)
                    .Include(c => c.Moves)
                    .ToList();
                return characters;
            }
        }

        //each player has a list of characters, but characters does not have a list of players despite it being a many to many relationship.
        //idea behind that is a player can play multiple characters but a character can also be played by multiple people. At most players can have up to 3 characters per match but characters
        //can be played by people worldwide.
        public List<Character> GetAllCharactersByPlayerId(int playID)
        {
            using (var _context = new GameContext())
            {
                var characters = new List<Character>();
                var player = _context.Players.Where(p => p.Id == playID)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character)
                            .ThenInclude(c => c.Moves)
                    .FirstOrDefault();
                
                foreach (PlayerCharacter pc in player.Characters)
                {
                    characters.Add(pc.Character);
                }

                return characters;
            }
        }

        public List<Character> GetAllCharactersByPlayerName(string name)
        {
            using (var _context = new GameContext())
            {
                var characters = new List<Character>();
                var player = _context.Players.Where(p => p.Name == name)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character)
                            .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach (PlayerCharacter pc in player.Characters)
                {
                    characters.Add(pc.Character);
                }

                return characters;
            }
        }

        public List<Character> GetAllCharactersByPlayer(Player player)
        {
            using (var _context = new GameContext())
            {
                var characters = new List<Character>();
                foreach(PlayerCharacter pc in player.Characters)
                {
                    characters.Add(pc.Character);
                }
                return characters;
            }
        }

        public List<Character> GetAllCharactersByMatchId(int matchID)
        {
            using (var _context = new GameContext())
            {
                var characters = new List<Character>();
                var match = _context.Matches.Where(m => m.Id == matchID)
                    .Include(m => m.Players)
                            .ThenInclude(pm => pm.Player)
                                .ThenInclude(p => p.Characters)
                                    .ThenInclude(c => c.Character)
                                        .ThenInclude(c => c.Moves)
                    .FirstOrDefault();

                foreach (PlayerMatch pm in match.Players)
                {
                    foreach (PlayerCharacter pc in pm.Player.Characters)
                    {
                        if (!characters.Contains(pc.Character))
                        {
                            characters.Add(pc.Character);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return characters;
            }
        }

        public List<Character> GetAllCharactersByTournamentId(int tournamentID)
        {
            using (var _context = new GameContext())
            {
                var characters = new List<Character>();
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
                    foreach (PlayerMatch pm in m.Players)
                    {
                        foreach (PlayerCharacter pc in pm.Player.Characters)
                        {
                            if (!characters.Contains(pc.Character))
                             {
                                characters.Add(pc.Character);
                             }
                             else
                             {
                                 continue;
                             }      
                            
                        }
                    }
                }

                return characters;
            }
        }


        public void UpdateCharacterName(Character character, string planet)
        {
            using (var _context = new GameContext())
            {
                character.Name = planet;
                _context.Characters.Update(character);
                _context.SaveChanges();
            }
        }

        public void UpdateCharacterPlanet(Character character, string name)
        {
            using (var _context = new GameContext())
            {
                character.Planet = name;
                _context.Characters.Update(character);
                _context.SaveChanges();
            }
        }

        //if a specialmove is added using the method in the SpecialMoveRepo without character, it can be assigned later with These two methods.
        public void UpdateCharacterMove(Character character, SpecialMove move)
        {
            using (var _context = new GameContext())
            {
                character.Moves.Add(move);
                _context.Characters.Update(character);
                _context.SaveChanges();
            }
        }

        public void UpdateCharacterMoves(Character character, List<SpecialMove> moves)
        {
            using (var _context = new GameContext())
            {
                character.Moves = moves;
                _context.Characters.Update(character);
                _context.SaveChanges();
            }
        }
        
        //deletes character and all associated special moves from the Character, PlayerCharacter, and Moves Tables respectively
        public void DeleteCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                //foreach(SpecialMove sp in character.Moves)
                //{
                //    _context.Moves.Remove(sp);
                //}
                var playerCharacters = _context.PlayerCharacter.Where(pc => pc.CharacterId == character.Id)
                    .Include(pc => pc.Character)
                        .ThenInclude(c => c.Moves)
                    .ToList();
                _context.PlayerCharacter.RemoveRange(playerCharacters);
                _context.Moves.RemoveRange(character.Moves);
                _context.Characters.Remove(character);
                _context.SaveChanges();
            }
        }

        public void DeleteManyCharacters(List<Character> characters)
        {
            using (var _context = new GameContext())
            {
                foreach(Character c in characters)
                {
                    //foreach(SpecialMove sp in c.Moves)
                    //{
                    //    _context.Moves.Remove(sp);
                    //}
                    var playerCharacters = _context.PlayerCharacter.Where(pc => pc.CharacterId == c.Id)
                        .Include(pc => pc.Character)
                            .ThenInclude(ch => ch.Moves)
                        .ToList();
                    _context.PlayerCharacter.RemoveRange(playerCharacters);
                    _context.Moves.RemoveRange(c.Moves);
                }
                _context.Characters.RemoveRange(characters);
                _context.SaveChanges();
            }
        }

        //Async
        ////Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var character = await _context.Characters
                    .Where(c => c.Id == id)
                    .Include(c => c.Moves)
                    .FirstOrDefaultAsync();
                return character;
            }
        }

        public async Task<List<Character>> GetCharacterByPlanetAsync(string planet)
        {
            using (var _context = new GameContext())
            {
                var characters = await _context.Characters.Where(c => c.Planet == planet)
                    .Include(c => c.Moves)
                    .ToListAsync();
                return characters;
            }
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            using (var _context = new GameContext())
            {
                var characters = await _context.Characters
                    .Include(c => c.Moves)
                    .ToListAsync();
                return characters;
            }
        }
    }
}
