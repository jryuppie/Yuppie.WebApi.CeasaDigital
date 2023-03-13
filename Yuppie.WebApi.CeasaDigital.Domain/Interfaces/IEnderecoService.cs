using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Endereco;

namespace Yuppie.WebApi.CeasaDigital.Domain.Interfaces
{
    public interface IEnderecoService
    {
        public Task<ObjectResult> AtualizarEndereco(EnderecoModel model);
        public Task<ObjectResult> CadastrarEndereco(EnderecoModel model);
        public Task<ObjectResult> BuscarEnderecoPorIdUsuario(int IdUsuario);
        public Task<ObjectResult> BuscarTodosEnderecos();
        public Task<ObjectResult> DesabilitarEndereco(int idEndereco);
        public Task<ObjectResult> ExcluirEndereco(int idEndereco);
    }
}
