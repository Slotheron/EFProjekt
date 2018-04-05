using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Player
    {
        public Player()
        {
            Matches = new List<PlayerMatch>();
            Characters = new List<PlayerCharacter>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public List<PlayerCharacter> Characters { get; set; }
        public List<PlayerMatch> Matches { get; set; }
    }
}
