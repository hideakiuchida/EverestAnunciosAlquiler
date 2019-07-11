using AutoMapper;
using Everest.Common.Extensions;
using Everest.Common.Settings;
using Everest.Repository.Implementations;
using Everest.Repository.Interfaces;
using Everest.Services.Implementations;
using Everest.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Everest.AnunciosAlquiler
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Everest API", Version = "v1" });
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper();

            services.AddScoped<IDbConnection>(x => new SqlConnection(Configuration.GetConnectionString("SqlServerConnection")));

            RegisterServices(services);
            RegisterRepositories(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Everest Alquiler Anuncios API!"); });
            app.UseMvc();
        }

        #region Privates Methods
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAnuncioService, AnuncioService>();
            services.AddScoped<IEvaluacionService, EvaluacionService>();
            services.AddScoped<IImagenService, ImagenService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPromocionService, PromocionService>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IAnuncioDetalleRepository, AnuncioDetalleRepository>();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();
            services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
            services.AddScoped<IImagenRepository, ImagenRepository>();
            services.AddScoped<IUbicacionRepository, UbicacionRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITipoPropiedadRepository, TipoPropiedadRepository>();
            services.AddScoped<IPromocionAnuncioRepository, PromocionAnuncioRepository>();
        }
        #endregion
    }
}
