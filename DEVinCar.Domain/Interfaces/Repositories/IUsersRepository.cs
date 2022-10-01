using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        public IQueryable<User> QueryMethod();
        User GetByIdService(int id);
        public User CheckUserByEmail(string email);
        void InsertUser(User user);
        void RemoveUser(User userRemoved);
    }
}