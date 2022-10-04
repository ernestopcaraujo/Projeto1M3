using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Infra.Data;

namespace DEVinCar.Infra.Data.Repositories
{
    public class CarsRepository : BaseRepository<Car,int>, ICarsRepository
    {
        public CarsRepository(DevInCarDbContext context) : base(context)
        {
            
        }

        public bool CheckCar(Car newCar)
        {
            var checkedCar = _context.Cars.Any(c => c.Name == newCar.Name || newCar.SuggestedPrice <= 0);

            return(checkedCar);
        }

        public bool CheckCarName(CarDTO carDTO,int carId)
        {
            var carCheckedName = _context.Cars.Any(c => c.Name == carDTO.Name && c.Id != carId);

            return(carCheckedName);
        }

        public void UpdateCar(CarDTO carDTO, int carId)
        {
            var carUpdated = _context.Cars.FirstOrDefault(c=>c.Id == carId);
            carUpdated.Name = carDTO.Name;
            carUpdated.SuggestedPrice = carDTO.SuggestedPrice;
            _context.SaveChanges();
        }
    }

}