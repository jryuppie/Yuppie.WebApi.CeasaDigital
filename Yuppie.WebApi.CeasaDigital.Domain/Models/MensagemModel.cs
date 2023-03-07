using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models
{
    public class MensagemModel
    {
        public MensagemModel()
        {
            Prefixo = "55";
        }
        public int IdComprador { get; set; }
        public int IdVendedor { get; set; }
        public int IdOferta { get; set; }
        public int IdProduto { get; set; }
        public bool EnvioComprador { get; set; }        
        public string Prefixo { get; set; }
    }
}
