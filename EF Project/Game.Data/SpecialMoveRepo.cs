using Game.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class SpecialMoveRepo
    {
        public void AddSpecialMove(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                if (spMove.CharacterId == 0)
                {
                    Console.WriteLine("SpecialMove: " + spMove.Name + " has not been assigned to any character. Operation aborted.");
                }
                else
                {
                    _context.Moves.Add(spMove);
                    _context.SaveChanges();
                }
            }
        }

        public void AddSpecialMoveWithoutCharacter(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.Add(spMove);
                _context.SaveChanges();
            }
        }
        
        public void AddSpecialMoves(List<SpecialMove> spMoves)
        {
            using (var _context = new GameContext())
            {
                List<SpecialMove> verifiedMoves = new List<SpecialMove>();
                foreach(SpecialMove sp in spMoves)
                {
                    if (sp.CharacterId == 0)
                    {
                        Console.WriteLine("SpecialMove: " + sp.Name + " has not been assigned to any character.");
                    }
                    else
                    {
                        verifiedMoves.Add(sp);
                    }
                }
                _context.Moves.AddRange(verifiedMoves);
                _context.SaveChanges();
            }
        }

        public void AddSpecialMovesWithoutCharacter(List<SpecialMove> spMoves)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.AddRange(spMoves);
                _context.SaveChanges();
            }
        }

        public List<SpecialMove> GetAllSpecialMoves()
        {
            using (var _context = new GameContext())
            {
                var moves = _context.Moves.ToList();
                return moves;
            }
        }

        public SpecialMove GetSpecialMoveById(int id)
        {
            using (var _context = new GameContext())
            {
                var move = _context.Moves.FirstOrDefault(sp => sp.Id == id);
                return move;
            }
        }

        public List<SpecialMove> GetSpecialMovesByCharacterId(int id)
        {
            using (var _context = new GameContext())
            {
                var moves = _context.Moves.Where(sp => sp.Id == id).ToList();
                return moves;
            }
        }

        public List<SpecialMove> GetSpecialMovesByCharacter(Character character)
        {
            using (var _context = new GameContext())
            {
                var moves = _context.Moves.Where(sp => sp.Id == character.Id).ToList();
                return moves;
            }
        } 
        
        public void UpdateSpecialMoveCharacter(SpecialMove spMove, int id)
        {
            using (var _context = new GameContext())
            {
                spMove.CharacterId = id;
                _context.Moves.Update(spMove);
                _context.SaveChanges();
            }
        }

        public void UpdateSpecialMoveLevel(SpecialMove spMove, int level)
        {
            using (var _context = new GameContext())
            {
                spMove.Level = level;
                _context.Moves.Update(spMove);
                _context.SaveChanges();
            }
        }

        public void UpdateSpecialMoveName(SpecialMove spMove, string name)
        {
            using (var _context = new GameContext())
            {
                spMove.Name = name;
                _context.Moves.Update(spMove);
                _context.SaveChanges();
            }
        }
        
        public void DeleteSpecialMove(SpecialMove spMove)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.Remove(spMove);
                _context.SaveChanges();
            }
        }

        public void DeleteManySpecialMoves(List<SpecialMove> spMoves)
        {
            using (var _context = new GameContext())
            {
                _context.Moves.RemoveRange(spMoves);
                _context.SaveChanges();
            }
        }

        //Async
        ////Chose to have async for the Get methods because data is not being updated or changed having a reduced chance to break.
        public async Task<SpecialMove> GetSpecialMoveByIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var move = await _context.Moves.FirstOrDefaultAsync(sp => sp.Id == id);
                return move;
            }
        }

        public async Task<List<SpecialMove>> GetSpecialMovesByCharacterIdAsync(int id)
        {
            using (var _context = new GameContext())
            {
                var moves = await _context.Moves.Where(sp => sp.Id == id).ToListAsync();
                return moves;
            }
        }

        public async Task<List<SpecialMove>> GetAllSpecialMovesAsync()
        {
            using (var _context = new GameContext())
            {
                var moves = await _context.Moves.ToListAsync();
                return moves;
            }
        }
    }
}
