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
        void RemoveBase (Car carRemoved);
        bool CheckCarName(CarDTO carDTO, int carId);
        void UpdateCar(CarDTO carDTO, int carId);
        public IQueryable<Car> QueryCar();
        
    }
}