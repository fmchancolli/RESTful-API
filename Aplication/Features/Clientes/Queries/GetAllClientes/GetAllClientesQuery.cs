using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            private readonly IMapper _mapper;

            public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<ClienteDTO>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
            {
                //obtiene un listado de clientes en base a la specificacion
                var clientes =await _repositoryAsync.ListAsync(new PagedSpecification(request.PageSize, request.PageNumber, request.Nombre, request.Apellido));
                var clientesDTO=_mapper.Map<List<ClienteDTO>>(clientes);


                return new PagedResponse<List<ClienteDTO>>(clientesDTO,request.PageNumber,request.PageSize);   
            }
        }
    }
}
