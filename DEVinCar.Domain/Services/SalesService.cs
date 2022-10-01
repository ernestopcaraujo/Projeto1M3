using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Services
{
    public class SalesService : ISalesService
    {
        
        private readonly ISalesRepository _salesRepository;
        private readonly IUsersService _usersService;

        public SalesService (ISalesRepository salesRepository, IUsersService usersService)
    
        {   _salesRepository = salesRepository;
            _usersService = usersService;
        }
        public void InsertSale(Sale sale)
        {
            var seller = _usersService.GetByIdService(sale.SellerId);
 
            if (seller == null)
            {
                throw new NotExistsException("The Seller does not exist!");
            }

            var buyer = _usersService.GetByIdBuyService(sale.BuyerId);
            if (buyer == null)
            {
                throw new NotExistsException("The Buyer does not exist!");
            }

            if (sale.SaleDate == null)
            {
                sale.SaleDate = DateTime.Now;
            }

            _salesRepository.InsertSale(sale);
        }
        
        public void InsertBuy(Sale sale)
        {
            var buyer = _usersService.GetByIdService(sale.BuyerId);

            if (buyer == null)
            {
                throw new NotExistsException("The Buyer does not exist!");
            }

            var seller = _usersService.GetByIdService(sale.SellerId);
            
            if (seller == null)
            {
                throw new NotExistsException("The Seller does not exist!");
            }
            
            if (sale.SaleDate == null)
            {
                sale.SaleDate = DateTime.Now;
            }

            _salesRepository.InsertSale(sale);
        }
    }
}