using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesReturnsSystem.BLL;
using SalesReturnsSystem.DAL;
using SalesReturnsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReturnsSystem
{
    public static class SalesReturnsExtensions
    {
        public static void AddSalesReturnDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<eBike_2025Context>(options);

            services.AddTransient<CustomerServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CustomerServices(context);
            });
            services.AddTransient<CustomerSaleServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CustomerSaleServices(context);
            });
            services.AddTransient<PartsServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new PartsServices(context);
            });
            services.AddTransient<RetrieveCategories>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new RetrieveCategories(context);
            });
            services.AddTransient<SalesServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new SalesServices(context);
            });
            services.AddTransient<InvoiceServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new InvoiceServices(context);
            });
            services.AddTransient<InvoiceDetailServices>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new InvoiceDetailServices(context);
            });
        }
    }
    
}
