using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SprinklingApp.Common.SerializationOperator;
using SprinklingApp.DataAccess;
using SprinklingApp.Service.EntityServices.Abstract;
using SprinklingApp.Service.EntityServices.Concrete;
using SprinklingApp.Service.Procedures.Abstract;
using SprinklingApp.Service.Procedures.Concrete;

namespace SprinklingApp.Master.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //string storageDirectory = Configuration.GetSection("StorageDirectory")?.Value;
            var codeBase = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var currentFolder = Path.GetDirectoryName(codeBase);
            services.TryAddScoped<IRepository>( x => new FileRepository<JsonSerialization>(currentFolder));

            #region [ EntityService ]
            services.TryAddScoped<IGroupService, GroupModelService>();

            #endregion

            #region [ Procedures ]
            services.TryAddScoped<IGroupProcedure, GroupProcedure>(); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
