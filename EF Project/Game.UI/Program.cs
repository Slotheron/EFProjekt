using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Data;
using Game.Domain;

namespace Game.UI
{
    class Program
    {
        private static PlayerRepo p = new PlayerRepo();
        private static CharacterRepo c = new CharacterRepo();
        static void Main(string[] args)
        {
            var newC = c.GetCharacterById(15);
            var x = p.GetAllPlayersByCharacterInTournament(16, newC);
        }
    }
}
