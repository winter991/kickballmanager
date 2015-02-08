using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KickballManager.Models
{
    public class LineupDetails
    {
        [Key]
        public int LineupDetailID { get; set; }
        public bool hasKicked { get; set; }
        public bool hasFielded { get; set; }
        public int PlayerID { get; set; }
        public int LineupID { get; set; }
        public virtual Lineup Lineup { get; set; }
        public virtual Player Player { get; set; }

    }
}
