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
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IOfertaService, OfertaService>();
            services.AddTransient<IVendaService, VendaService>();
            services.AddTransient<INegociacaoService, NegociacaoService>();
            services.AddTransient<IChatFirebaseService, ChatFirebaseService>();
            services.AddTransient<IUnMedidaService, UnMedidaService>();
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


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel, OfertaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Produto.ProdutoModel, ProdutoModel>();
                //mc.CreateMap<ChatFirebaseUserModel, Yuppie.WebApi.Infra.Models.Chat.ChatFirebaseUserModel>();

                //mc.CreateMap<ProcessoNegociacaoModel, Yuppie.WebApi.Infra.Models.Negociacao.ProcessoNegociacaoModel>();
                //mc.CreateMap<ProdutoModel, Yuppie.WebApi.Infra.Models.Produto.ProdutoModel>();
                //mc.CreateMap<ProdutoModel, Yuppie.WebApi.Infra.Models.Produto.ProdutoModel>();
                //mc.CreateMap<UnidadeMedidaModel, Yuppie.WebApi.Infra.Models.Produto.UnidadeMedidaModel>();
                //mc.CreateMap<UsuarioModel, Yuppie.WebApi.Infra.Models.UsuarioModel.UsuarioModel>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

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
             string connectionUrl = Configuration.GetConnectionString("DefaultConnection");
            return connectionUrl;
            var retorno = "";
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

