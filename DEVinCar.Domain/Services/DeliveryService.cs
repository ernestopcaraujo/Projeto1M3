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
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }
        public List<Delivery> GetDelivery(int? addressId, int? saleId)
        {
            var query = _deliveryRepository.QueryBase();

            if (addressId.HasValue)
            {
                query = query.Where(a => a.AddressId == addressId);
            }

            if (saleId.HasValue)
            {
                query = query.Where(s => s.SaleId == saleId);
            }
                      
            if (!query.ToList().Any())
            {
                throw new NotFoundException("No Delivery found !");
            }

            return query.ToList();
            
        }

    }
}