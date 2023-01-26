using PizzaApp.Server.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Server.DAL
{
    public class PizzaRepository : IDisposable
    {
        private readonly PizzaContext _DAL;

        public PizzaRepository(PizzaContext context)
        {
            _DAL = context;
            Users = new UserRepository(_DAL);
            Roles = new RolesRepository(_DAL);
        }

        public UserRepository Users { get; private set; }
        public RolesRepository Roles { get; private set; }

        public int Complete()
        {
            return _DAL.SaveChanges();
        }

        public void Dispose()
        {
            _DAL?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
