using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        private readonly DevInCarDbContext _context;
        public UsersRepository(DevInCarDbContext context)
        {
            _context = context;
        }
        public IList<User> GetByNameService(string name, DateTime birthDateMax, DateTime birthDateMin)
        {
            throw new NotImplementedException();
        }
         public IQueryable<User> QueryMethod()
        {
            return _context.Set<User>().AsQueryable();
        }
        public User GetByIdService(int id)
        {
            var user = _context.Users.Find(id);
            return (user);
        }
        public User CheckUserByEmail (string email)
        {
            var checkedUser = _context.Users.FirstOrDefault(u => u.Email == email);
            return(checkedUser);
        }
        public void InsertUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void RemoveUser(User userRemoved)
        {
            _context.Users.Remove(userRemoved);
            _context.SaveChanges();
        }
    }
}