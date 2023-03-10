using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public partial class BusinessStartup
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext, MsSqlDbContext>();
        }
    }
}
