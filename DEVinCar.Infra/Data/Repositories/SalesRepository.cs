using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{

    
    public class SalesRepository : BaseRepository<Sale,int>,ISalesRepository
    {

        
        public SalesRepository(DevInCarDbContext context) : base(context)
        {
            
        }
        
        public IList<Sale> GetByIdBuyService(int userId)
        {
            var sales = _context.Sales.Where(s => s.BuyerId == userId);
            return (sales.ToList());
        }
        public IList<Sale> GetSalesBySellerIdService(int userId)
        {
            var sales = _context.Sales.Where(s => s.SellerId == userId);
            return (sales.ToList());
        }
        public bool CheckSoldCar(int carId)
        {
            var checkedSoldCar = _context.SaleCars.Any(s => s.CarId == carId);

            return(checkedSoldCar);
        }

        public bool GetBuyerId(int buyerId)
        {
            var checkedBuyerId = _context.Sales.Any(s => s.BuyerId == buyerId);

            return(checkedBuyerId);
        }

        public bool GetSellerId(int sellerId)
        {
            var checkedSellerId = _context.Sales.Any(s => s.SellerId == sellerId);

            return(checkedSellerId);
        }


    }
}