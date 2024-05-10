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

        public JugadoresModel(int id, string nombre, string apellidos, string posicion, string avatar, EquipoModel equipo)
        {
            Jugador_id = id;
            Nombre = nombre;
            Apellidos = apellidos;              
            Posicion = posicion;
            Avatar = avatar;
            EquiposModel = equipo;
        }

    }

    

    public class EquipoModel
    {
        public int Equipo_id { get; set; }
        public string Nombre { get; set; }
        public string Escudo { get; set; }

        public EquipoModel(int equipo_id, string nombre, string escudo)
        {
            Equipo_id = equipo_id;
            Nombre = nombre;
            Escudo = escudo;
        }
    }
}
