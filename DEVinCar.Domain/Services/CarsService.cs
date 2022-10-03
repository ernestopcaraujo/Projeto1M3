using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DEVinCar.Domain.Services
{
    public class CarsService
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IMemoryCache _cache;
        public CarsService(ICarsRepository carsRepository, IMemoryCache cache)
        {
            _carsRepository = carsRepository;
            _cache = cache;
        }
 
        public Car GetById(int id){

            Car car = _carsRepository.GetByIdBase(id);
            if (car == null)
            {
                throw new NotExistsException("This car does not exists !");
            }

            if (!_cache.TryGetValue<Car>($"car:{id}", out car)){
                car = _carsRepository.GetByIdBase(id);
                _cache.Set($"car:{id}", car, new TimeSpan(0,10,0));
            }
            return (car);
        }
    }
}