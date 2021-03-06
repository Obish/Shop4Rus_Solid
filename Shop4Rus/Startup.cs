using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shop4Rus.Core;
using Shop4Rus.Core.Customer;
using Shop4Rus.Core.Discount;
using Shop4Rus.Core.Invoice;
using Shop4Rus.Interface;
using Shop4Rus.Interface.Discount;

namespace Shop4Rus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
          //  services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            // services.Add(DiscountPolicy discountpolicy);
            services.AddSwaggerGen();

            // services.AddScoped<ICreateCustomer>((x =>
            //       new CreateCustomer(x.GetRequiredService<IMapper>(),
            // x.GetRequiredService<ILogger>())));
            services.AddScoped<ICreateCustomer, CreateCustomer>();
            services.AddScoped<IGetCustomer, GetCustomer>();
            services.AddScoped<ICalculateInvoice, DiscountSystem>();
          //  services.AddScoped<IDiscountPoliciesMethods, CalculateDiscountForEachType>();
            services.AddScoped<ICreateNewDiscount, DiscountCore>();
            services.AddScoped<IGetDiscount, GetDiscount>();
            services.AddScoped<ICalculateDiscount, BaseDiscount>();
            services.AddScoped<ICalculateDiscount, CustomeTypeDiscount>();
            services.AddScoped<ICalculateDiscount, CustomerLoyaltyDiscount>();
            services.AddScoped<IProcessDiscountTypes, CalculateTotalDiscount>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
;            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
