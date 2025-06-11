using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServicingSystem.BLL;
using ServicingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicingSystem
{
    public static class ServicingExtension
    {
        public static void AddServicingDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<eBike_2025Context>(options);

            services.AddTransient<CustomerService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CustomerService(context);
            });

            services.AddTransient<CustomerVehicleService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CustomerVehicleService(context);
            });

            services.AddTransient<StandardJobsService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new StandardJobsService(context);
            });

            services.AddTransient<PartService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new PartService(context);
            });

            services.AddTransient<CategoryService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CategoryService(context);
            });

            services.AddTransient<CouponService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new CouponService(context);
            });

            services.AddTransient<JobService>((ServiceProvider) =>
            {
                //  Retrieve an instance of HogWildContext from the service provider.
                var context = ServiceProvider.GetService<eBike_2025Context>();

                // Create a new instance of WorkingVersionsService,
                //   passing the HogWildContext instance as a parameter.
                return new JobService(context);
            });


        }
    }
}
