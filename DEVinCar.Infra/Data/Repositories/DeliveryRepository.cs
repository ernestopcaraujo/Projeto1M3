using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DevInCarDbContext _context;
        public DeliveryRepository(DevInCarDbContext context)
        {
            _context = context;
        }
        public IQueryable <Delivery> QueryMethod()
        {
            return _context.Set<Delivery>().AsQueryable();
        }
    }
}