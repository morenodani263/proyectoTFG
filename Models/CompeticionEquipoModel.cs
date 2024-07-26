using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateMatch.Models
{
    class CompeticionEquipoModel
    {

        [JsonProperty("competicionid")]
        public CompeticionModel CompeticionId { get; set; }

        [JsonProperty("equipoid")]
        public EquipoModel EquipoId { get; set; }
    }
}
