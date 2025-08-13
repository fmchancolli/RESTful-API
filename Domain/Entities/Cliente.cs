using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public  class Cliente :AuditableBaseEntity
    {
        private int _edad;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string? Email { get; set; }
        public string Direccion { get; set; }

        public int Edad
        {
            get
            {
                if (this._edad <= 0)
                {
                    this._edad = new DateTime(DateTime.Now.Subtract(this.FechaNacimiento).Ticks).Year - 1;
                }

                return this._edad;
            }
            set
            {
                this._edad = value;
            }

            //get {
            //        if (this._edad <= 0 && FechaNacimiento!=DateTime.MinValue)
            //        {
            //            var hoy = DateTime.Today;
            //            _edad = hoy.Year - FechaNacimiento.Year;
            //            //si aun no ha cumplido años, restar 1
            //            if (FechaNacimiento.Date > hoy.AddYears(-_edad))
            //            {
            //                _edad--;
            //            }

            //            //this._edad = new DateTime(DateTime.Now.Subtract(this.FechaNacimiento).Ticks).Year - 1;
            //        }
            //        return this._edad; 
            //    }
            //    set
            //    {
            //        this._edad = value;
            //    }


        }


    }
}
