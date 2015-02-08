using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickballManager.Models
{
    public class Lineup
    {
        public int LineupID {get;set;}
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<LineupDetails> LineupDetails { get; set; }
    }
}