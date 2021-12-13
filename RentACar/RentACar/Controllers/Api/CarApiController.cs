using Microsoft.AspNetCore.Mvc;
using RentACar.Models.ViewModels;
using RentACar.Services;
using System;

namespace RentACar.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarApiController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarApiController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Function for adding vehicles. It checks the commonresponse and will send the connected message from the class helper.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>CommonResponse</returns>
        [HttpPost]
        [Route("SaveVehicle")]
        public IActionResult SaveVehicle(CarViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _carService.StoreCar(data).Result;
                if (commonResponse.Status == 1)
                {
                    // Update vehicle success
                    commonResponse.Message = "Voertuig is aangepast";
                }
                else if (commonResponse.Status == 2)
                {
                    // Succesful added vehicle
                    commonResponse.Message = "Voertuig is toegevoegd";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }
            return Ok(commonResponse);
        }
    }
}
