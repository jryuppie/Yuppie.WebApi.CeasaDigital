using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario
{
    public class EnderecoFormulario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Radius { get; set; }
        public bool Status { get; set; }
        public string Complemento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
