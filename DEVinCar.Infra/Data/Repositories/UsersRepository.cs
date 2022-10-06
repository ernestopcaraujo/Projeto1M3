using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{
    public class UsersRepository : BaseRepository<User,int>, IUsersRepository
    {

        public UsersRepository(DevInCarDbContext context) : base(context)
        {
            
        }
        public User CheckUserByEmail (string email)
        {
            var checkedUser = _context.Users.FirstOrDefault(u => u.Email == email);
            return(checkedUser);
        }

        public IQueryable<User> QueryUser()
        {
               var query = _context.Set<User>().AsQueryable();

            return query;
        }
    }
}