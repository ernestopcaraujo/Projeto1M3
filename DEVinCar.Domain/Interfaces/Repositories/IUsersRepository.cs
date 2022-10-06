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
        public IQueryable<User> QueryUser();
        User GetByIdBase(int id);
        public User CheckUserByEmail(string email);
        void InsertBase(User user);
        void RemoveBase(User userRemoved);
    }
}