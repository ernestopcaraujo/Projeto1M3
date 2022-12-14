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
        private readonly ISalesRepository _salesRepoistory;

        public CarsService(
            ICarsRepository carsRepository,
            IMemoryCache cache,
            ISalesRepository salesRepository
        )
        {
            _carsRepository = carsRepository;
            _cache = cache;
            _salesRepoistory = salesRepository;
        }

        public Car GetById(int id)
        {
            var car = _carsRepository.GetByIdBase(id);
            if (car == null)
            {
                throw new NotExistsException("This car does not exists !");
            }

            if (!_cache.TryGetValue<Car>($"car:{id}", out car))
            {
                car = _carsRepository.GetByIdBase(id);
                _cache.Set($"car:{id}", car, new TimeSpan(0, 5, 0));
            }
            return (car);
        }

        public IList<Car> GetList(string name, decimal? priceMin, decimal? priceMax)
        {
            var query = _carsRepository.QueryCar();

            if ((priceMin == 0)&&(priceMax == 0)&&(string.IsNullOrEmpty(name)))
            {
                query = _carsRepository.QueryCar();
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));

            }

            if ((priceMin != 0)&&(priceMax != 0)&&(priceMin > priceMax))
            {
                throw new IncompatibleValuesException(
                    $"The minimum price {priceMin} is higer {priceMax} than maximum price !"
                );
            }

            if ((priceMin != 0) && (priceMax == 0))
            {
                query = query.Where(c => c.SuggestedPrice >= priceMin);
            }

            if ((priceMin == 0) && (priceMax != 0))
            {
                query = query.Where(c => c.SuggestedPrice <= priceMax);
            }
            
            if (query.Count()==0)
            {
                throw new NotExistsException("No data found !");
            }
            
            return query.ToList();
        }

        public void InsertCar(Car newCar)
        {

            var checkedCar = _carsRepository.CheckCar(newCar);
            if (checkedCar == true)
            {
                throw new IncompatibleValuesException(
                    "Car price is not right or the car already exists !"
                );
            }
            _carsRepository.InsertBase(newCar);
            
        }

        public void RemoveCar(int carId)
        {
            var carRemoved = _carsRepository.GetByIdBase(carId);

            if (carRemoved == null)
            {
                throw new NotExistsException("This car does not exists !");
            }

            var carSold = _salesRepoistory.CheckSoldCar(carId);

            if (carSold == true)
            {
                throw new NotExistsException("This car was sold !");
            }

            _carsRepository.RemoveBase(carRemoved);

        }

        public void PutCar(CarDTO carDTO, int carId)
        {
            var carUpdated = _carsRepository.GetByIdBase(carId);

            if (carUpdated == null)
            {
                throw new NotExistsException("This car does not exists !");
            }

            if (carDTO.Name.Equals(null) || carDTO.SuggestedPrice.Equals(null))
            {
                throw new IncompatibleValuesException("The car name or car price is empty !");
            }
            if (carDTO.SuggestedPrice <= 0)
            { 
                throw new IncompatibleValuesException("The car name or car price is zero !");
            
            }
            var carCheckedName = _carsRepository.CheckCarName(carDTO, carId);

            if (carCheckedName == true)
            {
                throw new AlreadyExistsException("This car name already exists !");
            }

            _carsRepository.UpdateCar(carDTO,carId);

        }

        public Car GetByIdCache(int id)
        {
            var carCache =_carsRepository.GetByIdBase(id);

            return (carCache);
        }

    }
}
