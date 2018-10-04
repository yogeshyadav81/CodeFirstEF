using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstPrimer.Models.NHL;

namespace CodeFirstPrimer.Repository
{
   public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Team> TeamRepository { get; set; }
        IBaseRepository<Player> PlayerRepository { get; set; }
        void Save();
    }
}
