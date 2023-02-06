using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Services;
using Yuppie.WebApi.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Yuppie.WebApi.Infra.Context;
using Microsoft.OpenApi.Models;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital
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
            #region Services           
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IOfertaService, OfertaService>();
            services.AddTransient<IVendaService, VendaService>();
            #endregion

            #region Repositories
            services.AddTransient<IOfertaRepository, OfertaRepository>();
            services.AddTransient<IUsuarioRepository,UsuarioRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();
            services.AddTransient<IProcessoNegociacaoRepository, ProcessoNegociacaoRepository>();
            services.AddTransient<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            #endregion

            #region Context
            services.AddDbContext<PostGreContext>
           (options =>
           options.UseNpgsql(GetConnectionString()));
            services.AddControllers();
            #endregion


            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ceasa Digital API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ceasa Digital API");
            });
        }


        string GetConnectionString()
        {
            var retorno = "";
            string connectionUrl = Configuration.GetConnectionString("DefaultConnection");
            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');

            string[] userInfo = databaseUri.UserInfo
                                .Split(':', StringSplitOptions.RemoveEmptyEntries);

            retorno = $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};" +
                   $"Port={databaseUri.Port};Database={db};Pooling=true;" +
                   $"SSL Mode=Require;Trust Server Certificate=True;";

            return retorno;
        }
    }
}

