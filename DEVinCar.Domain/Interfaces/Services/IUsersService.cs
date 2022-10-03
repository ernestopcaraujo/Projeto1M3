using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        IList<User> GetByNameService(string name, DateTime? birthDateMax, DateTime? birthDateMin);
        User GetByIdService(int id);
        IList<Sale> GetByIdBuyService(int userId);
        IList<Sale> GetSalesBySellerIdService(int userId);
        void RemoveUser(int userId);
        void InsertUser(User newUser);

    }
}