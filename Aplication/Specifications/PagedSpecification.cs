using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
   public class PagedSpecification :Specification<Cliente>
    {
        public PagedSpecification(int pageSize, int pageNumber, string nombre, string apellido)
        {
            //calcular el skip
            //obtenemos datos segmentados por las paginas que le proporciones
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, $"%{nombre}%");

            if (!string.IsNullOrEmpty(apellido))
                Query.Search(x => x.Apellido, $"%{apellido}%");
        }
    }
}
