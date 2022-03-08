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
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IPaymentService _paymentService;

        public RentalsController(IRentalService rentalService, IPaymentService paymentService)
        {
            _rentalService = rentalService;
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, Rental rental)
        {
            rental.Id = id;
            var result = _rentalService.Update(rental);
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
            var rental = _rentalService.Get(id).Data;
            var result = _rentalService.Delete(rental);
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
            var result = _rentalService.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getallrentaldetails")]
        public ActionResult GetAllRentalDetails()
        {
            var result = _rentalService.GetAllRentalDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getallundeliveredrentaldetails")]
        public ActionResult GetAllUndeliveredRentalDetails()
        {
            var result = _rentalService.GetAllUndeliveredRentalDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getalldeliveredrentaldetails")]
        public ActionResult GetAllDeliveredRentalDetails()
        {
            var result = _rentalService.GetAllDeliveredRentalDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("deliverthecar")]
        public ActionResult DeliverTheCar(Rental rental)
        {
            var result = _rentalService.DeliverTheCar(rental);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
