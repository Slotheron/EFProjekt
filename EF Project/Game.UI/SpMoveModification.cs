using Game.Data;
using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class SpMoveModification
    {
        private static GameContext _context = new GameContext();

        public static void AddSpecialMove()
        {
            var thisCharacter = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Broly"));
            SpecialMove newMove = new SpecialMove();
            newMove.Name = "Eraser Cannon";
            newMove.Level = 1;
            newMove.CharacterId = thisCharacter.Id;
            
            _context.Moves.Add(newMove);
            _context.SaveChanges();
            Console.WriteLine("\nId:" + newMove.Id + "\nName: " + newMove.Name + " has been added to " + thisCharacter.Name + " in the database.");
        }

        //in future add console interface for user input
        public static void AddSpecialMoves()
        {
            var thisCharacter1 = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Broly"));
            SpecialMove newMoveB1 = new SpecialMove();
            newMoveB1.Name = "Gigantic Meteor";
            newMoveB1.Level = 3;
            newMoveB1.CharacterId = thisCharacter1.Id;

            var thisCharacter2 = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Vegeta"));
            SpecialMove newMoveV1 = new SpecialMove();
            newMoveV1.Name = "Big Bang Attack";
            newMoveV1.Level = 1;
            newMoveV1.CharacterId = thisCharacter2.Id;

            SpecialMove newMoveV2 = new SpecialMove();
            newMoveV2.Name = "Final Flash";
            newMoveV2.Level = 3;
            newMoveV2.CharacterId = thisCharacter2.Id;

            var thisCharacter3 = _context.Characters.FirstOrDefault(c => c.Name.StartsWith("Trunks"));
            SpecialMove newMoveT1 = new SpecialMove();
            newMoveT1.Name = "Burning Attack";
            newMoveT1.Level = 1;
            newMoveT1.CharacterId = thisCharacter3.Id;

            SpecialMove newMoveT2 = new SpecialMove();
            newMoveT2.Name = "Heat Dome Attack";
            newMoveT2.Level = 1;
            newMoveT2.CharacterId = thisCharacter3.Id;

            List<SpecialMove> moveList = new List<SpecialMove> { newMoveB1, newMoveV1, newMoveV2, newMoveT1, newMoveT2 };
            
            _context.Moves.AddRange(moveList);
            _context.SaveChanges();

            foreach (SpecialMove move in moveList)
            {
                var thisChar = _context.Characters.FirstOrDefault(c => c.Id == move.CharacterId);
                Console.WriteLine("\nId:" + move.Id + "\nName: " + move.Name + " has been added to " + thisChar.Name + " in the database.");
            }
        }

        public static void GetAllSpecialMoves()
        {
            var moves = _context.Moves.ToList();

            foreach (var m in moves)
            {
                Console.WriteLine("\nId:" + m.Id + "\nName: " + m.Name + "\nLevel: " + m.Level);
            }
        }

        public static void FindSpecialMove()
        {
            var move1 = _context.Moves.FirstOrDefault(c => c.Name.StartsWith("Final"));
            var move2 = _context.Moves.FirstOrDefault(c => c.Name.StartsWith("Heat"));
            Console.WriteLine("\nId:" + move1.Id + "\nName: " + move1.Name + "\nLevel: " + move1.Level);
            Console.WriteLine("\nId:" + move2.Id + "\nName: " + move2.Name + "\nLevel: " + move2.Level);
        }

        //possibility for multithreading here
        public static void UpdateSpecialMove()
        {
            var move = _context.Moves.FirstOrDefault(m => m.Name.StartsWith("Heat"));
            move.Level = 3;
            _context.Moves.Update(move);
            _context.SaveChanges();
            Console.WriteLine("\nId" + move.Id + "\nName: " + move.Name + "'s Level has been updated to " + move.Level + ".");
        }

        public static void UpdateSpecialMoveDisconnected()
        {
            var newContext = new GameContext();
            var move = _context.Moves.FirstOrDefault(m => m.Name.StartsWith("Heat"));
            move.Level = 3;
            newContext.Moves.Update(move);
            newContext.SaveChanges();
            Console.WriteLine("\nId" + move.Id + "\nName: " + move.Name + "'s Level has been updated to " + move.Level + ".");
        }

        public static void DeleteSpecialMove()
        {
            var move = _context.Moves.FirstOrDefault(m => m.Name.StartsWith("Heat"));
            _context.Moves.Remove(move);
            _context.SaveChanges();
            Console.WriteLine("\nId" + move.Id + "\nName: " + move.Name + " has been removed from the database.");
        }

        public static void DeleteManySpecialMoves()
        {
            var moves = _context.Moves.Where(m => m.Name.StartsWith("Final") || m.Name.StartsWith("Burning"));

            _context.Moves.RemoveRange(moves);
            _context.SaveChanges();

            foreach (SpecialMove m in moves)
            {
                Console.WriteLine("\n" + m.Id + ": " + m.Name + " has been removed from the database.");
            }
        }

        public static void DeleteManySpecialMovesDisconnected()
        {
            var newContext = new GameContext();
            var moves = _context.Moves.Where(m => m.Name.StartsWith("Final") || m.Name.StartsWith("Burning"));

            newContext.Moves.RemoveRange(moves);
            newContext.SaveChanges();

            foreach (SpecialMove m in moves)
            {
                Console.WriteLine("\n" + m.Id + ": " + m.Name + " has been removed from the database.");
            }
        }
    }
}
