using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Application.Features.Clientes.Queries.GetAllClientes;
using Application.Features.Clientes.Queries.GetClienteById;
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

        //Get API/<Controller>
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllClientesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientesQuery 
            { 
                PageNumber = filter.PageNumber,
                PageSize= filter.PageSize,
                Nombre = filter.Nombre,
                Apellido = filter.Apellido,
            }));
        }

        //POS Api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        { 
        return Ok(await Mediator.Send(command));
        }

        //PUT Api/<controller>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,UpdateClienteCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));

        }

        //PUT Api/<controller>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            //asigna el valor de la propiedad
            return Ok(await Mediator.Send(new DeleteClienteCommand { Id=id}));

        }

        //Get API/<Controller>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery { Id=id }));
        }
    }
}
