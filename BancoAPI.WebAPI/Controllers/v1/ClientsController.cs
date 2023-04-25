using BancoAPI.Application.Features.Clients.Commands;
using BancoAPI.Application.Features.Clients.Queries;
using BancoAPI.Application.Features.Clients.Queries.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllClientsParameters parameters)
        {
            var response = await Mediator.Send(new GetAllClientsQuery
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                Name = parameters.Name,
                LastName = parameters.LastName
            });
            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetClientByIdQuery { Id = id });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(command);
            }
            var response = await Mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteClientCommand { Id = id });
            return Ok(response);
        }
    }
}
