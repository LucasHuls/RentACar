using Microsoft.AspNetCore.Identity;
using RentACar.Models;
using RentACar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public CarService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _db = db;
            _userManager = userManager;
            _userService = userService;
        }

        public List<Car> GetCarList()
        {
            var cars = _db.Cars.ToList();
            
            return cars;
        }

        public async Task<int> StoreCar(CarViewModel model)
        {

            if (model != null && _db.Cars.Any(x => x.Kenteken == model.Kenteken))
            {
                // Vehicle already exists
                return 1;
            }
            else
            {
                // Create new vehicle
                Car vehicle = new Car
                {
                    Kenteken = model.Kenteken,
                    Merk = model.Merk,
                    Type = model.Type,
                    Dagprijs = model.Dagprijs
                };
                _db.Cars.Add(vehicle);
                await _db.SaveChangesAsync();
                return 2;
            }
        }
    }
}
