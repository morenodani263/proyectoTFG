using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class EstadioModel
    {
        public int Estadio_id { get; set; }
        public string Nombre { get; set; }
        public int Equipo_id { get; set; }

        public EstadioModel() { }
    }
}
