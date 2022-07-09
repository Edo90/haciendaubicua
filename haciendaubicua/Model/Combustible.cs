using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haciendaubicua.Model
{
    public class Combustible
    {
        public int Id { get; set; }
        public string TipoDePersona { get; set; } = "Juridica";
        public string Empresa { get; set; } = string.Empty;
        public int Galones { get; set; } = 0;
        public long Costo { get; set; } = 0;
        public double Itbis { get; set; } = 0;
        public DateTime CreateTime { get; set; }
    }
}
