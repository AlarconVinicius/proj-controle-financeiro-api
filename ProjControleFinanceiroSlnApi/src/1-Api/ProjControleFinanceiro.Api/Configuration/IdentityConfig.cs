using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjControleFinanceiro.Api.Data;

namespace ProjControleFinanceiro.Api.Configuration
{
    public static class IdentityConfig 
    {

       public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration) 
       {

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();



            return services;

       }




    }
}
