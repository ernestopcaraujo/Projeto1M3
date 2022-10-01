using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{

    
    public class SalesRepository : ISalesRepository
    {

        private readonly DevInCarDbContext _context;
        public SalesRepository(DevInCarDbContext context)
        {
            _context = context;
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
        public void InsertSale(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();

        }
        public void InsertBuy(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }
    }
}