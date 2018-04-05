using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Character
    {
        public Character()
        {
            Moves = new List<SpecialMove>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        // 1-12
        public int Color { get; set; }
        //point, mid, anchor
        public string Position { get; set; }
        public List<SpecialMove> Moves { get; set; }
    }
}
