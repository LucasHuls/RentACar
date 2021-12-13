using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Models.ViewModels;
using RentACar.Services;
using System;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult AllCars()
        {
            var cars = _carService.GetCarList();
            ViewBag.Vehicles = cars;
            return View();
        }

        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar(CarViewModel model)
        {
            await _carService.StoreCar(model);
            return View();
        }
    }
}
