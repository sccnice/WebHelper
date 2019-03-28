using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data2Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebHelper
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            AppConfigurtaionServices.SetConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           var IMvc=  services.AddMvc();
            IMvc.AddJsonOptions(options => options.SerializerSettings.ContractResolver
            = new LowercaseContractResolver());
            //services.Add
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class LowercaseContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}
