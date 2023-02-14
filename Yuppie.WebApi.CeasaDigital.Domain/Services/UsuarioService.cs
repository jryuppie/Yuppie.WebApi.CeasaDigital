using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public UsuarioModel BuscarUsuario(string user, string password)
        {
            try
            {
                return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(_usuarioService.BuscarUsuario(user, password))); ;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
