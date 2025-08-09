using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ApiException:Exception
    {
        //Solamente implementa el cosntructor de exception
        public ApiException():base(){}
        //Solamente pasa el mensaje de error
        public ApiException(string message) : base(message) { }

        //Pasa el mensaje de error y unos cuantos objetos tipo argumento el cual se lo pasamos al constructor de exception para que lo pinte
        public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,message,args)) { }



    }
}
