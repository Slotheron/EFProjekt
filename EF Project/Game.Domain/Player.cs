using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public List<Character> Characters { get; set; }
    }
}
