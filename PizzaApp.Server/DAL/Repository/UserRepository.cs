using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Server.DAL.Repository
{
    public class UserRepository
    {
        protected readonly PizzaContext _DAL;

        public UserRepository(PizzaContext context) => _DAL = context;

        public async Task InsertOrUpdate(User user)
        {
            try
            {
                if (user.ID == 0)
                {
                    await _DAL.Users.AddAsync(user);
                }
                else
                {
                    _DAL.Users.Update(user);
                }

                await _DAL.SaveChangesAsync();
            }
            catch(Exception e)
            {
                // todo - log message
            }
        }

        public async Task<bool> EmailAlreadyRegistered(string email)
        {
            return await _DAL.Users.Where(p => p.Email == email).AnyAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _DAL.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByID(int id)
        {
            return await _DAL.Users.Where(p => p.ID == id).FirstOrDefaultAsync();
        }
    }
}
