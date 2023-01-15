using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL.Models;

namespace PizzaApp.Server.DAL.Repository
{
    public class RolesRepository
    {
        protected readonly PizzaContext _DAL;

        public RolesRepository(PizzaContext context) => _DAL = context;

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _DAL.Roles.Where(p => p.Name == roleName).FirstOrDefaultAsync();
        }
    }
}
