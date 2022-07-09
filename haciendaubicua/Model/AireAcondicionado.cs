using System.ComponentModel.DataAnnotations.Schema;

namespace haciendaubicua.Model
{
    public class AireAcondicionado
    {
        public int Id { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public string TemperaturaActual { get; set; } = string.Empty;
        public string TemperaturaDeseada { get; set; } = string.Empty;
        [NotMapped]
        public TimeSpan HorasDeUso { get; set; } 
        public long HorasDeUsoTicks { get; set; }
        public double Consumo { get; set; }
        public double Costo { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
