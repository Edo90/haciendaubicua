using haciendaubicua.Model.Context;
using Serilog;

namespace haciendaubicua.Device
{
    public class AiresAcondicionado : IDevice
    {
        readonly ILogger logger;
        public AiresAcondicionado()
        {
            logger = Singleton.CustomLogger.GetInstance();
        }
        public async Task GenerateAsync()
        {
            List<Model.AireAcondicionado> aires = new();
            logger.Information("Iniciando Aires");
            //1
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(6, 10, 0), aires, "Contabilidad", 1.365, 11.05, TimeSpan.FromMinutes(90), TimeSpan.FromMinutes(0.1),"28C","24C");
            //2
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(6, 10, 0), aires, "Finanzas", 1.365, 11.05, TimeSpan.FromMinutes(90), TimeSpan.FromMinutes(0.1), "28C", "24C");
            //3
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(6, 10, 0), aires, "Despacho", 1.365, 11.05, TimeSpan.FromMinutes(90), TimeSpan.FromMinutes(0.1), "28C", "24C");
            //4
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(6, 10, 0), aires, "RRHH", 1.365, 11.05, TimeSpan.FromMinutes(90), TimeSpan.FromMinutes(1.27), "28C", "24C");

            //5
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(9, 10, 0), aires, "Contabilidad", 1.365, 11.05, TimeSpan.FromMinutes(180), TimeSpan.FromMinutes(0.1), "31C", "24C");
            //6
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(9, 20, 0), aires, "Finanzas", 1.365, 11.05, TimeSpan.FromMinutes(170), TimeSpan.FromMinutes(0.1), "31C", "24C");
            //7
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(8, 50, 0), aires, "Despacho", 1.365, 11.05, TimeSpan.FromMinutes(170), TimeSpan.FromMinutes(0.1), "31C", "24C");
            //8
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(8, 40, 0), aires, "RRHH", 1.365, 11.05, TimeSpan.FromMinutes(150), TimeSpan.FromMinutes(0.5), "31C", "24C");

            //9
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(5, 30, 0), aires, "Apagado", 1.365, 11.05, TimeSpan.FromMinutes(180), TimeSpan.FromMinutes(0.1), "28C", "24C");
            //10
            await GetAireAcondicionadoDeviceInfo(new TimeSpan(5, 40, 0), aires, "Apagado", 1.365, 11.05, TimeSpan.FromMinutes(170), TimeSpan.FromMinutes(0.1), "27C", "24C");
            logger.Information("Terminando proceso de aires");

            await SaveList(aires);
        }

        private async Task SaveList(List<Model.AireAcondicionado> airesAcondicionados)
        {
            try
            {
                using var context = new HaciendaDbContext();
                context.AireAcondicionado.AddRange(airesAcondicionados);
                await context.SaveChangesAsync();

                logger.Information($"{nameof(Model.AireAcondicionado)} guardado en la DB correctamente");
            }
            catch(Exception e)
            {
                logger.Error(@"error al tratar de guardar {1} - {0}",e.ToString(),nameof(AiresAcondicionado));
            }
           
        }

        private async Task GetAireAcondicionadoDeviceInfo(TimeSpan hourOfDay, List<Model.AireAcondicionado> aire, string departamento, double consumo, double costo, TimeSpan horasDeUso, TimeSpan minutesToDelay,string tempActual, string tempDeseada)
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = hourOfDay;
            today += timeSpan;


            await Task.Delay(minutesToDelay);
            aire.Add(new Model.AireAcondicionado
            {
                Consumo = 1.365,
                Costo = 11.09,
                HorasDeUso = new TimeSpan(1, 30, 0),
                HorasDeUsoTicks = horasDeUso.Ticks,
                CreatedTime = today,
                TemperaturaActual = tempActual
                ,TemperaturaDeseada = tempDeseada,
                Departamento = departamento
            }) ;

            logger.Information($"AC {departamento}");


        }

    }
}
