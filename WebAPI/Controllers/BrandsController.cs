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
    public class BrandsController : ControllerBase
    {
        IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            _brandService.Add(brand);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Brand brand)
        {
            brand.Id = id;
            _brandService.Update(brand);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var brand = _brandService.Get(id);
            _brandService.Delete(brand);
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var brand = _brandService.Get(id);
            return Ok(brand);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _brandService.GetAll();
            return Ok(brands);
        }
    }
}
