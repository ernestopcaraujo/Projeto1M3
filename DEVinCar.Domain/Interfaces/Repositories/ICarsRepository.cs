using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;

namespace DEVinCar.Domain.Interfaces.Repositories
{
    public interface ICarsRepository
    {
        Car GetByIdBase(int id);
        public IQueryable<Car> QueryBase();
        void InsertBase (Car newCar);
        bool CheckCar(Car newCar);
    }
}