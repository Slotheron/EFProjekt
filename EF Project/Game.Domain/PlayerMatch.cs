using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class PlayerMatch
    {
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        
        public Player Player { get; set; }
        public Match Match { get; set; }
    }
}
