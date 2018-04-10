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
        public static void AddPlayer(Player player)
        {
            using (var _context = new GameContext())
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }
        }

        public static void AddPlayer(Player player, Character character)
        {
            using (var _context = new GameContext())
            {
                _context.Players.Add(player);
                _context.Add(new PlayerCharacter{ Player = player, Character = character });
                _context.SaveChanges();
            }
        }
        
        public static void AddCharacterToPlayer(Player player, Character character)
        {
            using (var _context = new GameContext())
            {
                _context.PlayerCharacter.Add(new PlayerCharacter { PlayerId = player.Id, CharacterId = character.Id });
                _context.SaveChanges();
            }
        }
        public static void AddPlayers(List<Player> players)
        {
            using (var _context = new GameContext())
            {
                _context.Players.AddRange(players);
                _context.SaveChanges();
            }
        }

        public static List<Player> GetAllPlayers()
        {
            using (var _context = new GameContext())
            {
                var players = _context.Players
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Color)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Position).ToList();
                return players;
            }
        }

        public static Player FindPlayerById(int id)
        {
            using (var _context = new GameContext())
            {
                var player = _context.Players.Where(p => p.Id == id)
                    .Include(p => p.Characters)
                        .ThenInclude(pc => pc.Character)
                        .ThenInclude(c => c.Moves)
                    .ToList()
                    .FirstOrDefault();
                return player;
            }
        }

        public static void UpdatePlayerAge(Player player, int age)
        {
            using (var _context = new GameContext())
            {
                player.Age = age;
                _context.Players.Update(player);
                _context.SaveChanges();
            }
        }
        
        public static void UpdatePlayerNationality(Player player, string nationality)
        {
            using (var _context = new GameContext())
            {
                player.Nationality = nationality;
                _context.Players.Update(player);
                _context.SaveChanges();
            }
        }

        public static void DeletePlayer(Player player)
        {
            using (var _context = new GameContext())
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
        }

        public static void DeleteManyPlayers(List<Player> players)
        {
            using (var _context = new GameContext())
            {
                _context.Players.RemoveRange(players);
                _context.SaveChanges();
            }
        }
    }
}
