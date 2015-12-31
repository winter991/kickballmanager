using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KickballManager.Models;

namespace KickballManager.ViewModels
{
    public class AvailablePlayers
    {
        public int TeamID {get;set;}

        public string TeamName { get; set; }
        public ICollection<Player> Players { get; set; }
   
    }
}