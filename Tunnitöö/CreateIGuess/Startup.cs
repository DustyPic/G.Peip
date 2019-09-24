using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateIGuess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace CreateIGuess
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LasteDbContext>(options =>
            {
                //options.UseInMemoryDatabase(Guid.Empty.ToString());
                options.UseSqlite("Data Source = mydb.db");
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IMessageService, SmsMessenger>();
        }

        private void options(DbContextOptionsBuilder obj)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<LasteDbContext>();
                dbContext.Database.EnsureCreated();
                if (dbContext.Invoices.Count() == 0)
                {
                    var invoice = new Invoice()
                    {
                        Id = 1,
                        Date = DateTime.Now.Date,
                        InvoiceNo = "2019/120",
                        Lines = new List<InvoiceLine>()
                        {
                            new InvoiceLine
                            {
                                Id = 1,
                                LineItem = "Eesti juust",
                                Sum = 3.50
                            },
                            new InvoiceLine
                            {
                                 Id = 2,
                                 LineItem = "Ketšup",
                                 Sum = 2.50
                             }
                        }

                    };


                    dbContext.Invoices.Add(invoice);
                    dbContext.SaveChanges();
                }
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
