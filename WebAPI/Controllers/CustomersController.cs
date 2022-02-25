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
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            var result = _customerService.Add(customer);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Customer customer)
        {
            customer.Id = id;
            var result = _customerService.Update(customer);
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
            var customer = _customerService.Get(id).Data;
            var result = _customerService.Delete(customer);
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
            var result = _customerService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
