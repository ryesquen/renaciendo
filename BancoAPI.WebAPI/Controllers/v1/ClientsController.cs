using BancoAPI.Application.Features.Clients.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
