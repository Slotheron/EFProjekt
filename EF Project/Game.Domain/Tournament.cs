using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Tournament
    {
        public Tournament()
        {
            Matches = new List<Match>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public Double PrizeMoney { get; set; }
        public List<Match> Matches { get; set; }
    }
}
