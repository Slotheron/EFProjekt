using Game.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class SpecialMoveRepo
    {
        public static void AddSpecialMove(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.Add(spMove);
                _context.SaveChanges();
            }
        }
        
        public static void AddSpecialMoves(List<SpecialMove> spMoves)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.AddRange(spMoves);
                _context.SaveChanges();
            }
        }

        public static List<SpecialMove> GetAllSpecialMoves()
        {
            using (var _context = new GameContext())
            {
                var moves = _context.Moves.ToList();
                return moves;
            }
        }

        public static SpecialMove FindSpecialMoveById(int id)
        {
            using (var _context = new GameContext())
            {
                var move = _context.Moves.FirstOrDefault(sp => sp.Id == id);
                return move;
            }
        }

        public static List<SpecialMove> FindSpecialMovesByCharacterId(int id)
        {
            using (var _context = new GameContext())
            {
                var moves = _context.Moves.Where(sp => sp.Id == id).ToList();
                return moves;
            }
        }
        
        public static void UpdateSpecialMove(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.Update(spMove);
                _context.SaveChanges();
            }
        }

        public static void DeleteSpecialMove(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.Remove(spMove);
                _context.SaveChanges();
            }
        }

        public static void DeleteManySpecialMoves(List<SpecialMove> spMoves)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.RemoveRange(spMoves);
                _context.SaveChanges();
            }
        }
    }
}
