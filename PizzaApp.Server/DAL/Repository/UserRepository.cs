using PizzaApp.Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Server.DAL.Repository
{
    public class UserRepository
    {
        protected readonly PizzaContext _DAL;

        public UserRepository(PizzaContext context) => _DAL = context;

        public bool InsertOrUpdate(User user)
        {
            bool success;

            try
            {
                if (user.ID == 0)
                {
                    _DAL.Users.Add(user);
                }
                else
                {
                    _DAL.Users.Update(user);
                }

                _DAL.SaveChanges();

                success = true;
            }
            catch(Exception e)
            {
                // todo - log message
                success = false;
            }

            return success;
        }

        public bool EmailAlreadyRegistered(string email)
        {
            return _DAL.Users.Where(p => p.Email == email).Any();
        }

        public User? GetUserByEmail(string email)
        {
            return _DAL.Users.Where(p => p.Email == email).FirstOrDefault();
        }
    }
}
