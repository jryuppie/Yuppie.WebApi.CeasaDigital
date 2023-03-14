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
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using AutoMapper;
using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using FirebaseAdmin;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Endereco;
using Yuppie.WebApi.CeasaDigital.Swagger;

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
            #region Services           
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IOfertaService, OfertaService>();
            services.AddTransient<IVendaService, VendaService>();
            services.AddTransient<INegociacaoService, NegociacaoService>();
            services.AddTransient<IChatFirebaseService, ChatFirebaseService>();
            services.AddTransient<IUnMedidaService, UnMedidaService>();
            services.AddTransient<IWhatsappService, WhatsappService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IEnderecoService, EnderecoService>();
            #endregion

            #region Repositories
            services.AddTransient<IOfertaRepository, OfertaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();
            services.AddTransient<IProcessoNegociacaoRepository, ProcessoNegociacaoRepository>();
            services.AddTransient<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            #endregion

            #region Context
            services.AddDbContext<PostGreContext>
           (options =>
           options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            #endregion

            #region JWT
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    //options.Authority = Configuration["Authentication:JwtBearer:Authority"];
            //    //options.Audience = Configuration["Authentication:JwtBearer:Audience"];
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JwtBearer:TokenValidationParameters:IssuerSigningKey"])),
            //        ValidateIssuer = true,
            //        //ValidIssuer = Configuration["Authentication:JwtBearer:TokenValidationParameters:ValidIssuer"],
            //        ValidateAudience = true,
            //        //ValidAudience = Configuration["Authentication:JwtBearer:TokenValidationParameters:ValidAudience"],
            //        ValidateLifetime = true
            //    };
            //});
            #endregion

            #region Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.OfertaModel, OfertaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Produto.ProdutoModel, ProdutoModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.ProcessoNegociacaoModel, ProcessoNegociacaoModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Produto.UnidadeMedidaModel, UnidadeMedidaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Negociacao.VendaModel, VendaModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.UsuarioModel.UsuarioModel, UsuarioModel>();
                mc.CreateMap<UsuarioModel, Yuppie.WebApi.Infra.Models.UsuarioModel.UsuarioModel>();
                mc.CreateMap<Yuppie.WebApi.Infra.Models.Endereco.EnderecoModel, EnderecoModel>();
                mc.CreateMap<EnderecoModel, Yuppie.WebApi.Infra.Models.Endereco.EnderecoModel>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Cors and Swagger
            services.AddHttpClient();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ceasa Digital API", Version = "v2" });
                //c.OperationFilter<AuthHeaderOperationFilter>(); // Adiciona o filtro de autenticação JWT
            });
            #endregion



            #region FireBase
            var credentialsPath = Path.Combine(_env.ContentRootPath, "ceasawebchat-adminsdk.json");
            services.AddSingleton<FirebaseApp>(provider =>
            {
                var app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(credentialsPath),
                    ProjectId = "ceasawebchat"
                });
                return app;
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();        

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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

            //app.UseAuthentication();
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

