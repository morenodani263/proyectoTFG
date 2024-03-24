using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class UsuarioModel
    {
        public int Usuario_id { get; set; }
        [JsonProperty("nombreusuario")]
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        [JsonProperty("fechanacimiento")]
        public string FechaNacimiento { get; set; }
        public string Tfno { get; set; }
        public string Rol { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Calle { get; set; }
        public string CP { get; set; }
        public string Avatar { get; set; }
    }
}
