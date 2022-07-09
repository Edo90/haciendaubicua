using haciendaubicua.Model;
using haciendaubicua.Model.Context;
using Serilog;

namespace haciendaubicua.Device
{
    public class GPS : IDevice
    {
        private readonly ILogger logger;
        public GPS()
        {
            logger = Singleton.CustomLogger.GetInstance();
        }
        public async Task GenerateAsync()
        {
            List<VehiculosAsignados> vehiculosAsignados = new();
            logger.Information("Obteniendo Informacion de vehiculos asignados");
            //1
            await GetGpsDeviceInfo(new TimeSpan(6,30,0), vehiculosAsignados,"Hilux",1600,1450,100,"19.474067","-70.694090",TimeSpan.FromMinutes(1.35));
            //2
            await GetGpsDeviceInfo(new TimeSpan(6, 45, 0), vehiculosAsignados, "Hilux", 1600, 1300, 100, "19.390814", "-70.524556", TimeSpan.FromMinutes(0.02));
            //3
            await GetGpsDeviceInfo(new TimeSpan(7, 0, 0), vehiculosAsignados, "Hilux", 1600, 1450, 110, "19.474067", "-70.694090", TimeSpan.FromMinutes(0.02));
            //4
            await GetGpsDeviceInfo(new TimeSpan(7, 0, 0), vehiculosAsignados, "Prado", 1600, 1700, 115, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.02));
            //5
            await GetGpsDeviceInfo(new TimeSpan(11, 0, 0), vehiculosAsignados, "Prado", 1600, 1850, 120, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.83));
            //6
            await GetGpsDeviceInfo(new TimeSpan(11, 0, 0), vehiculosAsignados, "Prado", 1600, 1700, 115, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.01));
            //7
            await GetGpsDeviceInfo(new TimeSpan(11, 0, 0), vehiculosAsignados, "FJ", 1600, 1850, 120, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.01));
            //8
            await GetGpsDeviceInfo(new TimeSpan(17, 0, 0), vehiculosAsignados, "Land Cruiser", 2000, 2050, 120, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.83));
            //9
            await GetGpsDeviceInfo(new TimeSpan(17, 0, 0), vehiculosAsignados, "Land Cruiser", 1700, 1700, 115, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.01));
            //10
            await GetGpsDeviceInfo(new TimeSpan(17, 0, 0), vehiculosAsignados, "Tesla", 1800, 1000, 120, "18.474389", "-69.905146", TimeSpan.FromMinutes(0.01));
            logger.Information("Completando Informacion de vehiculos asignados");

            SaveList(vehiculosAsignados);
        }

        private void SaveList(List<Model.VehiculosAsignados> vehiculosAsignados)
        {
            try
            {
                using var context = new HaciendaDbContext();
                context.VehiculosAsignados.AddRange(vehiculosAsignados);
                context.SaveChanges();
                logger.Information($"{nameof(VehiculosAsignados)} guardado en la DB correctamente");
            }
            catch (Exception e)
            {
                logger.Error(@"error al tratar de guardar {1} - {0}", e.ToString(), nameof(VehiculosAsignados));
            }

        }
        private async Task GetGpsDeviceInfo(TimeSpan hourOfDay, List<VehiculosAsignados> vehiculosAsignados,string marca,int kmXMes,int kmsRecorridos,int velocidadMax,string longitude, string latitude, TimeSpan minutesToDelay)
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = hourOfDay;
            today += timeSpan;


            await Task.Delay(minutesToDelay);
            var status = kmsRecorridos > kmXMes ? "Detener" : "Estatus correcto";
            vehiculosAsignados.Add(new VehiculosAsignados
            {
                KmxMes = kmXMes,
                KmRecorrido = kmsRecorridos,
                VelocidadMaxRecorrida = velocidadMax,
                Marca = marca,
                Latitude = latitude,
                Longitude = longitude,
                Comentario = status,
                CreatedTime = today
            });

            logger.Information($"Informacion obtenida de {marca} vehiculo con estatus {status}");


        }
    }
}
