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
                    .FirstOrDefault();
                return character;
            }
        }

        public List<Character> GetCharacterByPlanet(string planet)
        {
            using (var _context = new GameContext())
            {
                var characters = _context.Characters.Where(c => c.Planet == planet).ToList();
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
        
        //deletes character and all associated special moves from the Character and Moves Tables respectively
        public void DeleteCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                //foreach(SpecialMove sp in character.Moves)
                //{
                //    _context.Moves.Remove(sp);
                //}
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
                    _context.Moves.RemoveRange(c.Moves);
                }
                _context.Characters.RemoveRange(characters);
                _context.SaveChanges();
            }
        }

        //Async
        ////Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<Character> FindCharacterByIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var character = await _context.Characters
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();
                return character;
            }
        }

        public async Task<List<Character>> FindCharacterByPlanetAsync(string planet)
        {
            using (var _context = new GameContext())
            {
                var characters = await _context.Characters.Where(c => c.Planet == planet).ToListAsync();
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
