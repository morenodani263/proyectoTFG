using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class JugadoresModel
    {
        public int Jugador_id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Posicion { get; set; }
        public EquipoModel EquiposModel { get; set; }
        public string Avatar { get; set; }

    }

    public class EquipoModel
    {
        public int Equipo_id { get; set; }
        public string Nombre { get; set; }
        public string Escudo { get; set; }
    }
}
