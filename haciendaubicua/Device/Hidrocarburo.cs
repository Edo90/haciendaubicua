

using haciendaubicua.Model;
using haciendaubicua.Model.Context;
using Serilog;

namespace haciendaubicua.Device
{
    internal class Hidrocarburo : IDevice
    {
        ILogger logger;
        public Hidrocarburo()
        {
            logger = Singleton.CustomLogger.GetInstance();
        }

        public async Task GenerateAsync()
        {
            List<Combustible> combustibles = new();

            

            logger.Information("Iniciando Hidrocarburos:" + DateTime.Now);
            ///1
            await GetHidrocarburosDeviceInfo(new TimeSpan(8, 10, 0), combustibles, 100, "La Ramba", "Juridica", TimeSpan.FromMinutes(1.69));
            ///2
            await GetHidrocarburosDeviceInfo(new TimeSpan(9, 00, 0), combustibles, 120, "FEDOMU", "Juridica", TimeSpan.FromMinutes(0.205));
            ///3
            await GetHidrocarburosDeviceInfo(new TimeSpan(9, 30, 0), combustibles, 135, "FENTRANO", "Juridica", TimeSpan.FromMinutes(0.104167));
            ///4
            await GetHidrocarburosDeviceInfo(new TimeSpan(10, 30, 0), combustibles, 50, "Pedro Baez", "Fisica", TimeSpan.FromMinutes(0.208333));
            ///5
            await GetHidrocarburosDeviceInfo(new TimeSpan(11, 00, 0), combustibles, 75, "Porfirio Acosta", "Fisica", TimeSpan.FromMinutes(0.104167));
            ///6
            await GetHidrocarburosDeviceInfo(new TimeSpan(12, 00, 0), combustibles, 225, "Juancito Sport", "Juridica", TimeSpan.FromMinutes(0.208333));
            ///7
            await GetHidrocarburosDeviceInfo(new TimeSpan(14, 00, 0), combustibles, 123, "La fe", "Juridica", TimeSpan.FromMinutes(0.416667));
            ///8
            await  GetHidrocarburosDeviceInfo(new TimeSpan(14, 30, 0), combustibles, 78, "La fe", "Juridica", TimeSpan.FromMinutes(0.104167));
            ///9
            await GetHidrocarburosDeviceInfo(new TimeSpan(15, 0, 0), combustibles, 200, "Ministerio de la Presidencia", "Juridica", TimeSpan.FromMinutes(0.104167));
            ///10
            await GetHidrocarburosDeviceInfo(new TimeSpan(16, 30, 0), combustibles, 200, "Ministerio de la Vivienda", "Juridica", TimeSpan.FromMinutes(0.375));


            logger.Information("Finalizando Hidrocarburos:" + DateTime.Now);

            await SaveList(combustibles);

        }
        private async Task SaveList(List<Model.Combustible> combustibles)
        {
            try
            {
                using var context = new HaciendaDbContext();
                context.Combustible.AddRange(combustibles);
                await context.SaveChangesAsync();
                logger.Information($"{nameof(Combustible)} guardado en la DB correctamente");
            }
            catch (Exception e)
            {
                logger.Error(@"error al tratar de guardar {1} - {0}", e.ToString(), nameof(Combustible));
            }

        }
        private async Task GetHidrocarburosDeviceInfo(TimeSpan hourOfDay, List<Combustible> combustibles, int galones,string empresa, string tipoPersona, TimeSpan minutesToDelay)
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = hourOfDay;
            today += timeSpan;


            await Task.Delay(minutesToDelay);
            combustibles.Add(new Combustible
            {
                Galones = galones,
                Costo = (long)galones * 297,
                Itbis = galones * 297 * 0.18,
                Empresa = empresa,
                TipoDePersona = tipoPersona,
                CreateTime = today
            });

            logger.Information($"Informacion obtenida de {empresa}");
          
            
        }
    }
}
