using haciendaubicua.Singleton;
using Microsoft.EntityFrameworkCore;



namespace haciendaubicua.Model.Context
{
    public class HaciendaDbContext : DbContext
    {
        public DbSet<Model.AireAcondicionado> AireAcondicionado { get; set; }
        public DbSet<Model.Combustible> Combustible { get; set; }
        public DbSet<Model.Loteria> Loteria { get; set; }
        public DbSet<Model.Pension> Pension { get; set;}
        public DbSet<Model.VehiculosAsignados> VehiculosAsignados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = AppConfiguration.GetInstance().ConnectionStrings.HaciendaDbConnectionString;
            builder.UseSqlServer(connectionString);
        }

    }
}
