using Application.Wrappers;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {

            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                ///atrapamos la peticion http que se genere por una llamada de api

                await _next(context);
            }
            catch (Exception error)
            {
                //1-interceptamos la respuesta
                //2-La modificamos dependiendo del error 
                //3-la arrojamos
                var response=context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string> { Succeded=false, Message=error?.Message };

                switch (error)
                {
                    case Application.Exceptions.ApiException e:
                        //custom apllication Error tipo aPI
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                        //obtinene los errores de validacion de flreunt validation
                    case Application.Exceptions.ValidationException e:
                        //custom apllication Error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;

                        break;

                        //Error 404 cuando no encontremos por ID nuestra info
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;

                        break;

                        //error no encontrado
                    default:
                        //unhandle error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        break;
                }
                 var result =JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);

            }
        }
    }
}
