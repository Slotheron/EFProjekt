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
        public static void AddCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.Add(character);
                _context.SaveChanges();
            }
        }
        
        public static void AddCharacters(List<Character> characters)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.AddRange(characters);
                _context.SaveChanges();
            }
        }

        public static List<Character> GetAllCharacters()
        {
            using (var _context = new GameContext())
            {
                var characters = _context.Characters
                    .Include(c => c.Moves)
                    .ToList();
                return characters;
            }
        }

        public static Character FindCharacterById(int id)
        {
            using (var _context = new GameContext())
            {
                var character = _context.Characters
                    .Where(c => c.Id == id)
                    .FirstOrDefault();
                return character;
            }
        }
        
        public static void UpdateCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.Update(character);
                _context.SaveChanges();
            }
        }

        public static void UpdateCharacterDisconnected(Character character)
        {
            using (var _context = new GameContext())
            {
                var newContext = new GameContext();
                newContext.Characters.Update(character);
                newContext.SaveChanges();
            }
        }

        public static void DeleteCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.Remove(character);
                _context.SaveChanges();
            }
        }

        public static void DeleteManyCharacters(List<Character> characters)
        {
            using (var _context = new GameContext())
            {
                _context.Characters.RemoveRange(characters);
                _context.SaveChanges();
            }
        }

        public static void DeleteManyCharactersDisconnected(List<Character> characters)
        {
            using (var _context = new GameContext())
            {
                var newContext = new GameContext();
                newContext.Characters.RemoveRange(characters);
                newContext.SaveChanges();
            }
        }
    }
}
