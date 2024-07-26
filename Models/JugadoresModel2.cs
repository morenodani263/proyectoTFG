using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateMatch.Models;

namespace UltimateMatch
{
    class JugadoresModel2
    {
        
            public int Jugador_id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Posicion { get; set; }
            public string Nacionalidad { get; set; }
            public EquipoModel EquiposModel { get; set; }
            public ImageSource Avatar { get; set; }

            public JugadoresModel2()
            {
                Jugador_id = 1;
                Nombre = "";
                Apellidos = "";
                Posicion = "";
                Avatar = "";
                EquiposModel = new EquipoModel();
                Nacionalidad = "";
            }
            public JugadoresModel2(int id, string nombre, string apellidos, string posicion, ImageSource avatar, EquipoModel equipo, 
                string nacion)
            {
                Jugador_id = id;
                Nombre = nombre;
                Apellidos = apellidos;
                Posicion = posicion;
                Avatar = avatar;
                EquiposModel = equipo;
                Nacionalidad = nacion;
            }

        }

    
}

