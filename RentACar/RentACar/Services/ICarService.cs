using RentACar.Models;
using RentACar.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface ICarService
    {
        /// <summary>
        /// Gets all cars
        /// </summary>
        /// <returns>List of Cars</returns>
        public List<Car> GetCarList();

        public Task<int> StoreCar(CarViewModel model);
        
    }
}
