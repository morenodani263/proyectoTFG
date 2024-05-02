using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class ClasificacionDTOModel
    {
        public string NombreCompeticion { get; set; }
        public string NombreEquipo { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int Puntos { get; set; }

        public ClasificacionDTOModel() { 
        
        }

    }
}
