namespace haciendaubicua.Model
{
    public class VehiculosAsignados
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public int VelocidadMaxRecorrida { get; set; }
        public int KmxMes { get; set; }
        public int KmRecorrido { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
