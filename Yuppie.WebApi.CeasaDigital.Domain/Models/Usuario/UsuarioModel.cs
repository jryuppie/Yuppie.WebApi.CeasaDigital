using System;
using Microsoft.EntityFrameworkCore;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public DateTime CreateDate { get; set; }
        public string Documento { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Sobrenome { get; set; }
        public bool Status { get; set; }
        public string Telefone { get; set; }
        public string TipoUsuario { get; set; }
        public string TipoPessoa { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
