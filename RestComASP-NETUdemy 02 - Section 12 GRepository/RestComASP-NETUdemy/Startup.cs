using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Business.Implementations;
using RestComASPNETUdemy.Model.Context;
using RestComASPNETUdemy.Repository;
using RestComASPNETUdemy.Repository.Generic;
using RestComASPNETUdemy.Repository.Implementations;
using System;
using System.Collections.Generic;

namespace RestComASP_NETUdemy
{
  public class Startup
  {
    private readonly ILogger iLogger;
    public IConfiguration Iconfiguration { get; }
    public IHostingEnvironment iEnvironment { get; }

    public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger) {

      Iconfiguration = configuration;
      iEnvironment = environment;
      iLogger = logger;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      var connectionString = Iconfiguration["MySqlConnection:MySqlConnectionString"];
      services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

      if (iEnvironment.IsDevelopment()) {

        try {

          var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
          var evolve = new Evolve.Evolve(evolveConnection, msg => iLogger.LogInformation(msg)) {

            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
          };

          evolve.Migrate();
        }
        catch (Exception ex) {

          iLogger.LogCritical("Banco de Dados migration failed.", ex);
          throw;
        }

      }

      services.AddMvc();
      services.AddApiVersioning(option => option.ReportApiVersions = true);

      services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
      services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
      services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
      //if (env.IsDevelopment()) {
      //  app.UseDeveloperExceptionPage();
      //}
      loggerFactory.AddConsole(Iconfiguration.GetSection("Logging"));
      loggerFactory.AddDebug();
      app.UseMvc();
    }
  }
}
