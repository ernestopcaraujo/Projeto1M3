using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Services
{   

    public class UsersService : IUsersService
    {
    
        private readonly IUsersRepository _usersRepository;
        private readonly ISalesRepository _salesRepository;
        public UsersService(IUsersRepository usersRepository, ISalesRepository salesRepository)
        {
            _usersRepository = usersRepository;
            _salesRepository = salesRepository;
        }

        public IList<User> GetByNameService(string name, DateTime? birthDateMax, DateTime? birthDateMin)
        {
            var query = _usersRepository.QueryMethod();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (birthDateMin.HasValue)
            {
                query = query.Where(c => c.BirthDate >= birthDateMin.Value);
            }

            if (birthDateMax.HasValue)
            {       
                query = query.Where(c => c.BirthDate <= birthDateMax.Value);
            }

            return query.ToList();
        }
        public User GetByIdService(int id)
        {
            var user = _usersRepository.GetByIdService(id);
            return (user);
        }

        public IList<Sale> GetByIdBuyService(int userId)
        {
           return _salesRepository.GetByIdBuyService(userId);
        }

        public IList<Sale> GetSalesBySellerIdService(int userId)
        {
            return _salesRepository.GetSalesBySellerIdService(userId);
        }

        public void InsertUser(User newUser)
        {
            var checkedUser = _usersRepository.CheckUserByEmail(newUser.Email);

            if (checkedUser != null)
            {
                throw new AlreadyExistsException("This user already exists !");
            }

            _usersRepository.InsertUser(newUser);

        }

        public void RemoveUser(int userId)
        {
            var userRemoved = _usersRepository.GetByIdService(userId);

            if (userRemoved == null)
            {
                throw new NotExistsException("This user does not exists !");
            }
        }
    }
}