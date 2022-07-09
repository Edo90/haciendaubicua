
using haciendaubicua.Utils;
using Serilog;
using Serilog.Core;

namespace haciendaubicua.Singleton
{
    public static class CustomLogger
    {
        private static Logger? instance;
        public static ILogger GetInstance()
        {
            if(instance == null)
            {
                var logger2 = new LoggerConfiguration().WriteTo.Console()
                                                        .WriteTo.File($"{Utils.Utils.GetFolderPath("Logs")}Logs.txt").CreateLogger();

                instance = logger2; 
            }

            return instance;
        }
    }
}
