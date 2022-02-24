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
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public IActionResult Add(Car car)
        {
            _carService.Add(car);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Car car)
        {
            car.Id = id;
            _carService.Update(car);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var car = _carService.Get(id);
            _carService.Delete(car);
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var car = _carService.Get(id);
            return Ok(car);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cars = _carService.GetAll();
            return Ok(cars);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetAllByDetail()
        {
            var cars = _carService.GetCarsDetail();
            return Ok(cars);
        }
    }
}
