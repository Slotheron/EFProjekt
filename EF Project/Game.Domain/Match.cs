using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Match
    {
        public int Id { get; set; }
        DateTime Time { get; set; }
        public int MaxRounds { get; set; }
    }
}
