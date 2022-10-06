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
                throw new NotFoundException("The Seller was not alpha found !");
            }

            var buyer = _usersService.GetByIdBuyService(sale.BuyerId);
            if (buyer == null)
            {
                throw new NotFoundException("The Buyer was not found !");
            }

            if (sale.SaleDate == null)
            {
                sale.SaleDate = DateTime.Now;
            }

            _salesRepository.InsertBase(sale);
        }
        
        public void InsertBuy(Sale sale)//Endpoint USER ID BUY
        {
            var buyer = _salesRepository.GetBuyerId(sale.BuyerId);

            if (buyer == false)
            {
                throw new NotFoundException("The Buyer was not found!");
            }

            var seller = _salesRepository.GetSellerId(sale.SellerId);
            
            if (seller == null)
            {
                throw new NotFoundException("The Seller was not found!");
            }
            
            if (sale.SaleDate == null)
            {
                sale.SaleDate = DateTime.Now;
            }

            _salesRepository.InsertBase(sale);
        }
    }
}