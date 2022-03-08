using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Car car)
        {
            car.Id = id;
            var result = _carService.Update(car);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var car = _carService.Get(id).Data;
            var result = _carService.Delete(car);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _carService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getcarsdetail")]
        public IActionResult GetCarsDetail()
        {
            var result = _carService.GetCarsDetail();
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getcardetail")]
        public IActionResult GetCarDetail(int carId)
        {
            var result = _carService.GetCarDetailAndImagesDto(carId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getbybrand")]
        public IActionResult GetByBrand(int brandId)
        {
            var result = _carService.GetCarsDetailByBrandId(brandId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }



        [HttpGet("getbycolor")]
        public IActionResult GetByColor(int colorId)
        {
            var result = _carService.GetCarsDetailByColorId(colorId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("getbybrandandcolor")]
        public IActionResult GetByBrandAndColor(int brandId, int colorId)
        {
            var result = _carService.GetCarsDetailByBrandIdAndColorId(brandId, colorId);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);

        }
    }
}
