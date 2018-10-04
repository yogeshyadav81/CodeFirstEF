using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;

using CodeFirstPrimer.Models.NHL;

namespace CodeFirstPrimer.Entities
{
    public class NhlContext : DbContext
    {
        public NhlContext() : base("DefaultConnection")
        {}

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}