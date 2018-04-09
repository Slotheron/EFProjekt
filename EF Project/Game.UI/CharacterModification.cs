using Game.Data;
using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class CharacterModification
    {
        private static GameContext _context = new GameContext();

        public static void AddCharacter()
        {
            Character newChar = new Character();
            newChar.Name = "Broly: The Legendary Super Saiyan";

            _context.Characters.Add(newChar);
            _context.SaveChanges();
            Console.WriteLine("\nId:" + newChar.Id + "\nName: " + newChar.Name + " has been added to the database.");
        }

        //in future add console interface for user input
        public static void AddCharacters()
        {
            Character newChar1 = new Character();
            newChar1.Name = "Kakarot";
            Character newChar2 = new Character();
            newChar2.Name = "Vegeta";
            Character newChar3 = new Character();
            newChar3.Name = "Trunks";
            Character newChar4 = new Character();
            newChar4.Name = "Krillin";
            Character newChar5 = new Character();
            newChar5.Name = "Ginyu";
            Character newChar6 = new Character();
            newChar6.Name = "A.Gohan";

            List<Character> CharList = new List<Character> { newChar1, newChar2, newChar3, newChar4, newChar5, newChar6 };
            _context.Characters.AddRange(CharList);
            _context.SaveChanges();
            foreach (Character c in CharList)
            {
                Console.WriteLine("\nId:" + c.Id + "\nName: " + c.Name + " has been added to the database.");
            }
        }

        public static void GetAllCharacters()
        {
            var characters = _context.Characters.ToList();

            foreach (var c in characters)
            {
                Console.WriteLine("\nId:" + c.Id + "\nName: " + c.Name);
            }
        }

        public static void FindCharacter()
        {
            var character1 = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Broly"));
            var character2 = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Kakarot"));
            Console.WriteLine("\nId:" + character1.Id + "\nName: " + character1.Name + " has been added to the database.");
            Console.WriteLine("\nId:" + character2.Id + "\nName: " + character2.Name + " has been added to the database.");
        }

        //possibility for multithreading here
        public static void UpdateCharacter()
        {
            string oldName = "Kakarot";
            var character = _context.Characters.FirstOrDefault(c => c.Name == oldName);
            character.Name = "Goku";
            _context.Characters.Update(character);
            _context.SaveChanges();
            Console.WriteLine("\nId" + character.Id + "\nName: " + oldName + "'s Name has been updated to " + character.Name + ".");
        }

        public static void UpdateCharacterDisconnected()
        {
            var newContext = new GameContext();
            string oldName = "Kakarot";
            var character = _context.Characters.FirstOrDefault(c => c.Name == oldName);
            character.Name = "Goku";
            newContext.Characters.Update(character);
            newContext.SaveChanges();
            Console.WriteLine("\nId" + character.Id + "\nName: " + oldName + "'s Name has been updated to " + character.Name + ".");
        }

        public static void DeleteCharacter()
        {
            var character = _context.Characters.FirstOrDefault(c => c.Name == "Krillin");
            _context.Characters.Remove(character);
            _context.SaveChanges();
            Console.WriteLine("\nId" + character.Id + "\nName: " + character.Name + " has been removed from the database.");
        }

        public static void DeleteManyCharacters()
        {
            var characters = _context.Characters.Where(p => p.Name == "Ginyu" || p.Name == "A.Gohan").ToList();

            _context.Characters.RemoveRange(characters);
            _context.SaveChanges();
            
            foreach (Character c in characters)
            {
                Console.WriteLine("\n" + c.Id + ": " + c.Name + " has been removed from the database.");
            }
        }

        public static void DeleteManyCharactersDisconnected()
        {
            var newContext = new GameContext();
            var characters = _context.Characters.Where(p => p.Name == "Ginyu" || p.Name == "A.Gohan").ToList();

            newContext.Characters.RemoveRange(characters);
            newContext.SaveChanges();

            foreach (Character c in characters)
            {
                Console.WriteLine("\n" + c.Id + ": " + c.Name + " has been removed from the database.");
            }
        }
    }
}
