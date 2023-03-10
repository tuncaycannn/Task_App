using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class MsSqlDbContext : ProjectDbContext
    {
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options, IConfiguration configuration)
       : base(options, configuration)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsContext")));
            }
        }
    }
}
