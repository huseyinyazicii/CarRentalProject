using Business.Abstract;
using Core.Entities.Concrete;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, User user)
        {
            user.Id = id;
            var result = _userService.Update(user);
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
            var user = _userService.Get(id).Data;
            var result = _userService.Delete(user);
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
            var result = _userService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("getbyemaildto")]
        public ActionResult GetByEmailDto(User user)
        {
            var result = _userService.GetByEmailDto(user.Email);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("getbyemail")]
        public ActionResult GetByEmail(User user)
        {
            var result = _userService.GetByEmail(user.Email);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
