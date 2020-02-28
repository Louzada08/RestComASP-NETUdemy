using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestComASPNETUdemy.Business;
using RestComASPNETUdemy.Business.Implementations;
using RestComASPNETUdemy.Model.Context;
using RestComASPNETUdemy.Repository;
using RestComASPNETUdemy.Repository.Generic;
using RestComASPNETUdemy.Repository.Implementations;
using RestComASPNETUdemy.Security.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;

namespace RestComASP_NETUdemy
{
  public class Startup
  {
    private readonly ILogger iLogger;
    public IConfiguration _configuration { get; }
    public IHostingEnvironment iEnvironment { get; }

    public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger) {

      _configuration = configuration;
      iEnvironment = environment;
      iLogger = logger;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      // Tempo de tolerância para a expiração de um token (usado no caso de
      // de problemas de sincronização de tempo entre diferentes
      // computadores envolvidos no processo de comunicação)

      var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
      services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

      ExecuteMigrations(connectionString);

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      var tokenConfigurations = new TokenConfiguration();

      new ConfigureFromConfigurationOptions<TokenConfiguration>(
          _configuration.GetSection("TokenConfigurations")
      )
      .Configure(tokenConfigurations);

      services.AddSingleton(tokenConfigurations);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
        paramsValidation.ValidAudience = tokenConfigurations.Audience;
        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

        // Validates the signing of a received token
        paramsValidation.ValidateIssuerSigningKey = true;

        // Checks if a received token is still valid
        paramsValidation.ValidateLifetime = true;

        // Tolerance time for the expiration of a token (used in case
        // of time synchronization problems between different
        // computers involved in the communication process)
        paramsValidation.ClockSkew = TimeSpan.Zero;
      });

      // Enables the use of the token as a means of
      // authorizing access to this project's resources
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            .RequireAuthenticatedUser().Build());
      });

      services.AddMvc(options => {
        options.RespectBrowserAcceptHeader = true;
        options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
        options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
      }).AddXmlSerializerFormatters();

      services.AddApiVersioning(option => option.ReportApiVersions = true);

      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1",
          new Info {
            Title = "RESTful API COM ASP.NET Core 2.0",
            Version = "v1"
          });
      });

      services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
      services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
      services.AddScoped<ILoginRepository, LoginRepositoryImpl>();

      services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
    }

    private void ExecuteMigrations(string connectionString) {
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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
      //if (env.IsDevelopment()) {
      //  app.UseDeveloperExceptionPage();
      //}
      loggerFactory.AddConsole(_configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseSwagger();
      app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
      });

      var option = new RewriteOptions();
      option.AddRedirect("^$", "swagger");
      app.UseRewriter(option);

      app.UseMvc(routes => {
        routes.MapRoute(
          name: "DefaultApi",
          template: "{controller=Values}/{id?}");
      });
    }
  }
}
