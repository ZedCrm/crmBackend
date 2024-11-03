using App.Contracts.Object.Base;
using App.Object.Base;
using ConfApp;
using ConfApp.Rep;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class CRMBootstraper
    {
        public static void AddCRMManagement(IServiceCollection service, string connectionstring)
        {
            //service.AddTransient<IProductApplication, ProductApplication>();
            service.AddTransient<IPersonApp, PersonApp>();
            service.AddTransient<IPersonRep, PersonRep>();


            service.AddDbContext<MyContext>(c =>
            {
                c.UseSqlServer(connectionstring, b => b.MigrationsAssembly("Infrastructure"));
            });

        }
    }
}
