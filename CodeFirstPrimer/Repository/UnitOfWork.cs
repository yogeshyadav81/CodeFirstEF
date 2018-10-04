using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstPrimer.Models.NHL;
using CodeFirstPrimer.Entities;

namespace CodeFirstPrimer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBaseRepository<Team> teamRepository { get; set; }
        private IBaseRepository<Player> playerRepository { get; set; }
        private NhlContext context = null;
        private bool disposed = false;
      
        public IBaseRepository<Team> TeamRepository
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new BaseRepository<Team>(context);
                return teamRepository;
            }
            set
            {
                teamRepository = value;
            }
        }

        public IBaseRepository<Player> PlayerRepository
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new BaseRepository<Player>(context);
                return playerRepository;
            }
            set
            {
                playerRepository = value;
            }
        }

        public UnitOfWork()
        {
            context = new NhlContext();

        }


        public void Save()
        {
            context.SaveChanges();
        }

        #region Dispose Methods
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}