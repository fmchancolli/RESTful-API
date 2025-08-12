using Application.Features.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClienteController : BaseAPIController
    {

        //enrrutar no tener  logica
        //la logica esta en el core
        //cuando se ejecute el send se ira en el handler
        //POS API/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        { 
        return Ok(await Mediator.Send(command));
        }
    }
}
