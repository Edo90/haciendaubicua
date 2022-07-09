
namespace haciendaubicua.Model
{
    public class Pension
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public bool EsEmpleado { get; set; }
        public bool Status { get; set; } = true;
        public double Monto { get; set; }
        public string Razon { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

    }
}
