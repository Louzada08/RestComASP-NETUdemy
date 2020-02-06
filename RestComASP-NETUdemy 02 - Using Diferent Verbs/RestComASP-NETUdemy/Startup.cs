using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestComASPNETUdemy.Model.Context;
using RestComASPNETUdemy.Services;
using RestComASPNETUdemy.Services.Implementations;

namespace RestComASP_NETUdemy
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      var connection = Configuration["MySqlConnection:MySqlConnectionString"];
      services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));
      services.AddMvc();

      services.AddScoped<IPersonService, PersonServiceImpl>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
