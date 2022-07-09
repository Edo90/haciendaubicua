using haciendaubicua.Model;
using Microsoft.Extensions.Configuration;

namespace haciendaubicua.Singleton
{
    public static class AppConfiguration
    {
        private static AppSettings? _instance;

        public static AppSettings GetInstance()
        {
            if(_instance == null)
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
                    .AddEnvironmentVariables()
                    .Build();

                _instance = config.Get<AppSettings>();
            }
            return _instance;
        }
    }
}
