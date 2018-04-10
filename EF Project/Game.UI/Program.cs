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
        static void Main(string[] args)
        {
            Character myCharacter = CharacterRepo.FindCharacterById(1);
            Player myPlayer = PlayerRepo.FindPlayerById(1);
        }

    }
}
