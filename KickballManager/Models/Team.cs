using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KickballManager.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; } 
        public String TeamName { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Lineup> Lineups { get; set; }
    }
}
