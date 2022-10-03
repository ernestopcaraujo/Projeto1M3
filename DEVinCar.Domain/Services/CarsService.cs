using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DEVinCar.Domain.Services
{
    public class CarsService : ICarsService
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

        public List<Car> GetList(string name, decimal? priceMin, decimal? priceMax)
        {
             var query = _carsRepository.QueryBase();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (priceMin > priceMax)
            {
                throw new IncompatibleValuesException("The minimum price is higer than maximum price !");
            }

            if (priceMin.HasValue)
            {
                query = query.Where(c => c.SuggestedPrice >= priceMin);
            }

            if (priceMax.HasValue)
            {
                query = query.Where(c => c.SuggestedPrice <= priceMax);
            }

            if (!query.ToList().Any())
            {
                throw new NotExistsException("This query has no result !");
            }

            return query.ToList();
        }
        public void InsertCar(Car newCar)
        {
            var checkedCar = _carsRepository.CheckCar(newCar);

            if (checkedCar == true)
            {
                throw new IncompatibleValuesException("Car price is not right or the car already exists !");
            }
        
            _carsRepository.InsertBase(newCar);

        }
    }    
}