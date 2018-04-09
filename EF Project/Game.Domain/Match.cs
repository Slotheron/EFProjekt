using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public class Match
    {
        public Match()
        {
            Players = new List<PlayerMatch>();
        }
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int MaxRounds { get; set; }
        public List<PlayerMatch> Players { get; set; }
        public int TournamentId { get; set; }
    }
}
