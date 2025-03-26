using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rommanel.Api.Controllers.Common;
using Rommanel.Api.Model;
using Rommanel.Application.Commands.CreateCustomerCommand;
using Rommanel.Application.Queries;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Api.Controllers
{
    [Route("api/customers")]
    public class CustomerController : MainController
    {
        public CustomerController(Notifiy.INotification notification, 
                                  IMediator mediator)
            :base(notification, mediator) { }

        [HttpGet]
        public async Task<ActionResult> GetStudentListAsync([FromQuery] CustomerDto query)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _mediator.Send(new GetCustomerByFilterQuery
            {
                Name = query.QueryField,    
                Email = query.QueryField,   
                Document = query.QueryField,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize

            });

            return CustomResponse(result);
        }

        [HttpGet("customerId")]
        public async Task<ActionResult> GetStudentByIdAsync(Guid customerId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _mediator.Send(new GetCustomerByIdQuery() { Id = customerId }));
        }

        [HttpPost]
        public async Task<ActionResult> AddStudentAsync([FromBody] CreateCustomerCommand command)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _mediator.Send(command));
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<ActionResult> UpdateStudentAsync([FromRoute] Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!Equals(command, null))
                command.Id = id;

            return CustomResponse(await _mediator.Send(command!));
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<ActionResult> DeleteStudentAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _mediator.Send(new DeleteCustomerCommand() { Id = id }));
        }
    }
}
