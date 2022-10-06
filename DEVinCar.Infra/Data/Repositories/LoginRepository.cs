using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Infra.Data;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Repositories;

namespace DEVinCar.Infra.Data.Repositories
{
    public class LoginRepository : BaseRepository<User,string>, ILoginRepository
    {
        public LoginRepository (DevInCarDbContext context) : base (context)
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