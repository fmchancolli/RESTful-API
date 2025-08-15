using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Queries.GetAllClientes
{
   public  class GetAllClientesQuery: IRequest<PagedResponse<List<ClienteDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }


        public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PagedResponse<List<ClienteDTO>>>
        {
            private readonly IRepositoryAsync<Cliente> _repositoryAsync;
            private readonly IDistributedCache _distributedCache;
            private readonly IMapper _mapper;

            public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }

            public async Task<PagedResponse<List<ClienteDTO>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
            {
                //se crea una llave por cada combinacion que el usuario haga
                var cacheKey =$"listadoClientes_{request.PageSize}_{request.PageNumber}_{request.Nombre}_{request.Apellido}";
                string serialaizerListadoclientes;
                var listadoClientes = new List<Cliente>();
                var redisListadoClientes = await _distributedCache.GetAsync(cacheKey);
                //hay datos en cache?
                if (redisListadoClientes != null)
                {
                    //si hay deserializa los datos 
                    serialaizerListadoclientes = Encoding.UTF8.GetString(redisListadoClientes);
                    listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(serialaizerListadoclientes);

                }
                else { 
                    //si no hay cosnsulta la base de datos 
                    // y pasa esos datos a una nueva cache
                    listadoClientes= await _repositoryAsync.ListAsync(new PagedSpecification(request.PageSize, request.PageNumber, request.Nombre, request.Apellido));
                    serialaizerListadoclientes=JsonConvert.SerializeObject(listadoClientes);
                    redisListadoClientes = Encoding.UTF8.GetBytes(serialaizerListadoclientes);

                    var options = new DistributedCacheEntryOptions()

                        //Se establece el tiempo de vencimiento del objeto almacenado en cache
                        //resumen: si se usa dura 10 minutos
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        //caduca como un objeto en cache, si no se solicita durante un periodo de tiempo definido
                        //resumen: si no se usa en 2 minutos expira
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await _distributedCache.SetAsync(cacheKey,redisListadoClientes,options);

                }

                    //obtiene un listado de clientes en base a la specificacion
                var clientesDTO=_mapper.Map<List<ClienteDTO>>(listadoClientes);


                return new PagedResponse<List<ClienteDTO>>(clientesDTO,request.PageNumber,request.PageSize);   
            }
        }
    }
}
