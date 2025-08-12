using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersioningExtensions(this IServiceCollection services)
        { 
        //extension para versionar nuestra API
                services.AddApiVersioning(config =>
                {
                    //define por default la version
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    //asuma por defecto cuando no este espeficicado
                    //si el cliente no especifoca la version que quiere consumir, por defecto la API va optener el numero
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    //
                    config.ReportApiVersions = true;
                });


        }
    }
}
