using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;

namespace DEVinCar.Infra.Data.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery,int>,IDeliveryRepository
    {
        public DeliveryRepository(DevInCarDbContext context) : base(context)
        {
            
        }
    }
}