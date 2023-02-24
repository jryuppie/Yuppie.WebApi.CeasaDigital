using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Yuppie.WebApi.Infra.Context;
using Microsoft.OpenApi.Models;
using Yuppie.WebApi.Infra.Repository;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using AutoMapper;
using FirebaseAdmin;

using System.IO;
using Google.Apis.Auth.OAuth2;

namespace Yuppie.WebApi.CeasaDigital
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var webRoot = _env.ContentRootPath;
            var credentialsPath = Path.Combine(webRoot, "ceasawebchat-adminsdk.json");

            #region Services           
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IOfertaService, OfertaService>();
            services.AddTransient<IVendaService, VendaService>();
            services.AddTransient<INegociacaoService, NegociacaoService>();
            services.AddTransient<IChatFirebaseService, ChatFirebaseService>();
            services.AddTransient<IUnMedidaService, UnMedidaService>();
            services.AddTransient<IChatFirebaseService, ChatFirebaseService>();
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
           options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            #endregion

            #region Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel, OfertaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Produto.ProdutoModel, ProdutoModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.ProcessoNegociacaoModel, ProcessoNegociacaoModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Produto.UnidadeMedidaModel, UnidadeMedidaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.VendaModel, VendaModel>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Cors and Swagger
               services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ceasa Digital API", Version = "v1" });
            });
            #endregion




            //Configuração do FirebaseAdmin

           //FirebaseApp.Create(new AppOptions()
           //{
           //    Credential = GoogleCredential.FromFile(credentialsPath),
           //});
           // services.AddSingleton(FirebaseApp.DefaultInstance);
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

            app.UseAuthentication();
        }


        string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        
            //string connectionUrl = Configuration.GetConnectionString("DefaultConnection");
            //var databaseUri = new Uri(connectionUrl);

            //string db = databaseUri.LocalPath.TrimStart('/');

            //string[] userInfo = databaseUri.UserInfo
            //                    .Split(':', StringSplitOptions.RemoveEmptyEntries);

            //retorno = $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};" +
            //       $"Port={databaseUri.Port};Database={db};Pooling=true;" +
            //       $"SSL Mode=Require;Trust Server Certificate=True;";

            //return retorno;
        }
    }
}

