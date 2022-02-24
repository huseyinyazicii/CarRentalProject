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
    public class ColorsController : ControllerBase
    {
        IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        public IActionResult Add(Color color)
        {
            _colorService.Add(color);
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Color color)
        {
            color.Id = id;
            _colorService.Update(color);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var color = _colorService.Get(id);
            _colorService.Delete(color);
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var color = _colorService.Get(id);
            return Ok(color);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var colors = _colorService.GetAll();
            return Ok(colors);
        }
    }
}
