using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class PlayerCharacter
    {
        public int PlayerId { get; set; }
        public int CharacterId { get; set; }

        public Player Player { get; set; }
        public Character Character { get; set; }
    }
}
