using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using Yuppie.WebApi.Infra.Context;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class BaseService : IBaseService
    {
        private readonly PostGreContext _context;
        public BaseService(PostGreContext context)
        {
            _context = context;
        }

        public UsuarioModel GetUser(string user, string password)
        {
            try
            {
                var t = 0;
                var userDB = _context.Usuarios.Where(x => x.telefone == user && x.senha == password).FirstOrDefault();
                return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(userDB));
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }



    }
}
