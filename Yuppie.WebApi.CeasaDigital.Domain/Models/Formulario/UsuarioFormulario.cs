using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario
{
    public class UsuarioFormulario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string TipoPessoa { get; set; }
        public string TipoUsuario { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }        
        public string Documento { get; set; }
        public bool Status { get; set; }
    }

    public class UsuarioStatusFormulario
    {
        public string Documento { get; set; }
        public bool Status { get; set; }
    }
}
