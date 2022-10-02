using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{
    public class AutenticationRepository : BaseRepository<User,string>
    {
        public AutenticationRepository (DevInCarDbContext context) : base (context)
        {
            
        }

        public User CheckUser(LoginDTO loginDTO)

        {
            var incomingUser = _context
                                .Users
                                .FirstOrDefault(u=>u.Email == loginDTO.Email &&
                                                u.Password == loginDTO.Password);
            return (incomingUser);
        }
    }
}