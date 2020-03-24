using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using notification_services.Application.UseCase.Notification.Create;
using notification_services.Application.UseCase.Notification.Update;
using notification_services.Application.UseCase.Notification.ReadBy;
using notification_services.Application.UseCase.Notification.ReadById;
using notification_services.Application.UseCase.Notification.ReadWithLog;
using notification_services.Application.UseCase.Notification.Delete;
using notification_services.Presistences;

namespace CustomerServices.Presenter.Controllers
{
    [ApiController]
    [Route("notification")]
    public class CustomerController : ControllerBase
    {
        private IMediator _mediatr;

        public CustomerController(IMediator Mediator)
        {
            _mediatr = Mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomer(string include)
        {
            if(include == "logs")
            {
            var result = new ReadNotificationWithLog();
            return Ok(await _mediatr.Send(result));
            }
            else
            {
            var result = new ReadNotification();
            return Ok(await _mediatr.Send(result));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer( CreateNotification payload)
        {
            var result = await _mediatr.Send(payload);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadNotificationIdDto>> GetCustomerById(int id)
        {
            var result = new ReadNotificationId(id);
            return Ok(await _mediatr.Send(result));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, UpdateNotification data)
        {
            data.Data.Attributes.Notification_id = ID;
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteNotification(id);
            var result = await _mediatr.Send(command);

            return result != null ? (ActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });

        }
    }
}