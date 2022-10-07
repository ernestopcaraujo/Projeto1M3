using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories
{
    public interface ISalesRepository
    {
        IList<Sale> GetByIdBuyService(int id);
        IList<Sale> GetSalesBySellerIdService(int userId);
        void InsertBase(Sale sale);
        bool CheckSoldCar(int carId);
        bool GetBuyerId(int buyerId);
        bool GetSellerId(int sellerId);

    }
}