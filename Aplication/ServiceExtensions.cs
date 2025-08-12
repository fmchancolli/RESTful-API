using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplication
{
   public static class ServiceExtensions
    {
        public static void AddAplicationLayer(this IServiceCollection services)
        {

            //servicios de automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //servicios de fluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //servicios de MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //registramos el validator del pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        }
    }
}
