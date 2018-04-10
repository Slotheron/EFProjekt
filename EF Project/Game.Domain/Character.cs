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
        public string Planet { get; set; }
        public List<SpecialMove> Moves { get; set; }
    }
}
