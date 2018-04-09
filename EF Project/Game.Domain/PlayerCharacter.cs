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

        // 1-12
        public int Color { get; set; }
        //point, mid, anchor
        public string Position { get; set; }

        public Player Player { get; set; }
        public Character Character { get; set; }
    }
}
