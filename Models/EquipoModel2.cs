using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    public class EquipoModel2
    {       
        public int Equipo_id { get; set; }
        public string Nombre { get; set; }
        public ImageSource Escudo { get; set; }

        public EquipoModel2(int equipo_id, string nombre, ImageSource escudo)
        {
            Equipo_id = equipo_id;
            Nombre = nombre;
            Escudo = escudo;
        }

        public EquipoModel2()
        {

        }
        
    }
}
