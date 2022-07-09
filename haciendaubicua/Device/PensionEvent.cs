
using haciendaubicua.Model;
using haciendaubicua.Model.Context;
using Serilog;

namespace haciendaubicua.Device
{
    public class PensionEvent : IDevice
    {
        private readonly ILogger logger;
        public PensionEvent()
        {
            logger = Singleton.CustomLogger.GetInstance();
        }
        public async Task GenerateAsync()
        {

            List<Pension> pensiones = new();
            logger.Information("Obteniendo informacion de Direccion General de Jubilaciones y Pensiones");
            //1
            await SuspenderPension(new TimeSpan(8, 0, 0), pensiones,"Perez","999-9999999-9",false,10000,"Ramon","Pension Militar", TimeSpan.FromMinutes(1.67));
            //2
            await SuspenderPension(new TimeSpan(9, 0, 0), pensiones, "Vasquez", "123-9999999-9", true, 6000, "Fela", "Empleada domestica", TimeSpan.FromMinutes(0.205));
            //3
            await SuspenderPension(new TimeSpan(10, 30, 0), pensiones, "Jose", "123-1239999-9", false, 25000, "Cesar", "Pension de la Presidencia", TimeSpan.FromMinutes(0.3125));
            //4
            await SuspenderPension(new TimeSpan(12, 0, 0), pensiones, "Bejaran", "123-1233229-9", false, 35000, "Augusto", "Coronel Retirado", TimeSpan.FromMinutes(0.208333333));
            //5
            await SuspenderPension(new TimeSpan(14, 0, 0), pensiones, "Velasquez", "123-8766234-9", true, 35000, "Victor", "Consultor Juridico", TimeSpan.FromMinutes(0.104166667));
            //6
            await SuspenderPension(new TimeSpan(14, 30, 0), pensiones, "Grullon", "123-8711111-9", true, 23000, "Arturo", "Programador", TimeSpan.FromMinutes(0.416666667));
            //7
            await SuspenderPension(new TimeSpan(15, 0, 0), pensiones, "Feliz", "333-8711111-9", true, 23500, "Laura", "Jurista", TimeSpan.FromMinutes(0.104166667));
            //8
            await SuspenderPension(new TimeSpan(15, 0, 0), pensiones, "Grullon", "123-8711123-9", false, 12500, "Bienvenido", "Ayudante Retirado", TimeSpan.FromMinutes(0.104166667));
            //9
            await SuspenderPension(new TimeSpan(15, 30, 0), pensiones, "Benitez", "123-8333111-9", false, 8000, "Samuel", "Ayudante Retirado", TimeSpan.FromMinutes(0.1));
            //10
            await SuspenderPension(new TimeSpan(16, 30, 0), pensiones, "Grullon", "123-87113241-9", false, 9000, "Bienvenido", "Ayudante Retirado", TimeSpan.FromMinutes(0.2083333));

            logger.Information("Actualizando pensiones e enviando informacion correspondiente");

            await SaveList(pensiones);

        }

        private async Task SaveList(List<Model.Pension> pensions)
        {
            try
            {
                using var context = new HaciendaDbContext();
                context.Pension.AddRange(pensions);
                await context.SaveChangesAsync();
                logger.Information($"{nameof(Pension)} guardado en la DB correctamente");
            }
            catch (Exception e)
            {
                logger.Error(@"error al tratar de guardar {1} - {0}", e.ToString(), nameof(Pension));
            }

        }

        private async Task SuspenderPension(TimeSpan hourOfDay, List<Pension> pensiones, string apellido, string cedula, bool esEmpleado, double monto, string nombre, string razon, TimeSpan minutesToDelay)
        {
            var today = DateTime.Now.Date;
            TimeSpan timeSpan = hourOfDay;
            today += timeSpan;


            await Task.Delay(minutesToDelay);

            var status = esEmpleado ? "Suspendido" : "Activo";
            var empleoEstatus = esEmpleado ? "ser empleado" : "no ser empleado";

            pensiones.Add(new Pension
            {
                Apellido = apellido,
                Cedula = cedula,
                EsEmpleado = esEmpleado,
                Status = !esEmpleado,
                Monto = monto,
                Nombre = nombre,
                Razon = razon,
                CreationDate = today
            });

            logger.Information($"Pension para {apellido}, {nombre} ha sido colocado con estatus {status} por este {empleoEstatus}");
        }
    }
}
