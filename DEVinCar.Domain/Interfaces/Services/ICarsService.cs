using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Services
{
    public interface ICarsService
    {
        Car GetById (int carId);
        IList<Car> GetList (string name, decimal? priceMin, decimal? priceMax );
        void InsertCar(Car newCar);
        void RemoveCar(int carId);
        void PutCar (CarDTO carDTO, int carId);
        
    }
}