using haciendaubicua.Model;
using haciendaubicua.Model.Context;
using Serilog;
using System.Diagnostics;

namespace haciendaubicua.Device
{
    public class BancaDeLoteria : IDevice
    {
        readonly ILogger logger;
        public BancaDeLoteria()
        {
            logger = Singleton.CustomLogger.GetInstance();
        }
        /// <summary>
        /// se obtendra latitud y longitud de las bancas junto con comentario para conocer ubicacion exacta de cada una
        /// </summary>
        /// <returns></returns>
        public async Task GenerateAsync()
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = new(8, 0, 0);
            today += timeSpan;
            
            List<Loteria> loterias = new();
            
            Console.WriteLine("Iniciando Banca de Loterias:" + DateTime.Now);

            await Task.Delay(TimeSpan.FromMinutes(1.67));
            ///1
            logger.Information("Banca Altagracia verificada");
            loterias.Add(new()
            {
                Comentario = "Banca Altagracia",
                Latitude = "18.490342",
                Longitude = "-69.892988",
                CreateTime = today,
            });
            await Task.Delay(TimeSpan.FromMinutes(0.309167));
            ///2
            ///
            today = DateTime.Now.Date;
            timeSpan = new(9, 30, 0);
            today += timeSpan;
            logger.Information($"Banca Perez verificada {today}");
            loterias.Add(new()
            {
                Comentario = "Banca Perez",
                Latitude = "18.490343",
                Longitude = "-69.892988",
                CreateTime = today,
            });
            await Task.Delay(TimeSpan.FromMinutes(0.309167));

            ///3
            ///
            await GetDeviceInfo("Banca Rosario", "Verificada", new(10, 0, 0), "18.490443", "-69.892988", TimeSpan.FromMinutes(0.104167), loterias);
            ///4
            ///
            await  GetDeviceInfo("Banca Peña", "No Encontrada", new(11, 0, 0), "18.493443", "-69.892988", TimeSpan.FromMinutes(0.208333), loterias);
            ///4
            ///
            await GetDeviceInfo("Banca Irregular", "Debe ser fiscalizada", new(12, 0, 0), "18.417443", "-69.892988", TimeSpan.FromMinutes(0.208333), loterias);
            ///5
            ///
            await GetDeviceInfo("Banca Perez", "Verificada", new(13, 30, 0), "18.417498", "-69.892988", TimeSpan.FromMinutes(0.3125), loterias);
            ///6
            ///
            await GetDeviceInfo("Banca la iluminada", "Verificada", new(14, 10, 0), "18.417412", "-69.892988", TimeSpan.FromMinutes(0.125), loterias);
            ///7
            ///
            await GetDeviceInfo("Banca la Altagracia", "Verificada", new(15, 0, 0), "18.417412", "-69.892921", TimeSpan.FromMinutes(0.1875), loterias);
            ///8
            ///
            await GetDeviceInfo("Banca Roman", "No Encontrada", new(15, 47, 0), "18.417412", "-69.892321", TimeSpan.FromMinutes(0.145833), loterias);
            ///9
            ///
            await GetDeviceInfo("Banca Penca", "Verificada", new(16, 0, 0), "18.417352", "-69.892321", TimeSpan.FromMinutes(0.0625), loterias);
            ///10
            ///
            await GetDeviceInfo("Banca IoT", "Verificada", new(17, 5, 0), "18.417352", "-69.892345", TimeSpan.FromMinutes(0.0625), loterias);

            await SaveList(loterias);
        }

        private async Task SaveList(List<Model.Loteria> loterias)
        {
            try
            {
                using var context = new HaciendaDbContext();
                context.Loteria.AddRange(loterias);
                await context.SaveChangesAsync();
                logger.Information($"{nameof(Loteria)} guardado en la DB correctamente");
            }
            catch (Exception e)
            {
                logger.Error(@"error al tratar de guardar {1} - {0}", e.ToString(), nameof(Loteria));
            }

        }

        public async Task GetDeviceInfo(string bancaName,string comment, TimeSpan hourOfDay, string latitud, string longitude, TimeSpan minutesFromInterval, List<Loteria> loterias)
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = hourOfDay;
            today += timeSpan;

            logger.Information($"{bancaName} {comment} {today}");
            loterias.Add(new()
            {
                Comentario = $"{bancaName} {comment}",
                Latitude = latitud,
                Longitude = longitude,
                CreateTime = today,
            });
            await Task.Delay(minutesFromInterval);
        }

    }
}
