
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using payment_services.Application.UseCase.payment.Create;
using payment_services.Application.UseCase.payment.Update;
using payment_services.Application.UseCase.payment.ReadBy;
using payment_services.Application.UseCase.payment.ReadById;
using payment_services.Application.UseCase.payment.Delete;


namespace payment_services.Controllers
{

    [ApiController]
    [Route("payments")]
    public class UsersController : ControllerBase
    {

        private IMediator _mediatr;

        public UsersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ReadPayment();
            return Ok(await _mediatr.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = new ReadPaymentId(id);
            return Ok(await _mediatr.Send(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePayment data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new Deletepayment(id);
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePayment data)
        {
            data.Data.Attributes.id = id;
            return Ok(await _mediatr.Send(data));
        }
    }

}