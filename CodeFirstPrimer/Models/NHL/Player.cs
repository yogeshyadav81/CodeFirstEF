﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstPrimer.Models.NHL
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        
        public string TeamName { get; set; }
        public virtual Team Team { get; set; }
    }
}