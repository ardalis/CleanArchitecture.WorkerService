using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class ContainerSetup
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        
        public static void AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IRepository, EfRepository>();
    }
}
