using CLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLData.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<bool> InsertCar(Car car);    
        Task<bool> UpdateCar(Car car);    
        Task<bool> DeleteCar(Car car);    
    }
}
