using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Queries.GetAllClientes
{
   public class GetAllClientesParameters: RequestParameter
    {
        //OJO: el signo (?) permite valores nullos, si no fluent validation dice que hay que llenar los campos
        public string? Nombre { get; set; }
        public string?  Apellido { get; set; }
    }
}
