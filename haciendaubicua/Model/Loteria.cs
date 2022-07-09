using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haciendaubicua.Model
{
    public class Loteria
    {
        public int Id { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        //public double Radio { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
    }
}
