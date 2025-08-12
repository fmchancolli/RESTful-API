using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //versiona nuestra llamadas al API

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //podamos consumir nuestro controlador dependiendo de la version
    public abstract class BaseAPIController : ControllerBase
    {
       private IMediator _mediator;
        //injeccion directa del mediator
        //cualquiera que herede de BaseAPIController lo podra implemantar en su controlador
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
