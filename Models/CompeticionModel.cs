using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class CompeticionModel
    {
        [JsonProperty("competicion_id")]
        public int Competicion_Id { get; set; }
        public string Nombre { get; set; }
        public bool Es_Liga { get; set; }
        public string Avatar { get; set; }

        public CompeticionModel() {
            Avatar = "";
            Es_Liga = true;
        }
    }
}
