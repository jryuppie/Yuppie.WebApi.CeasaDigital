using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.Infra.Models.Negociacao
{
    public class VendaModel
    {
        public int id { get; set; }
        public int id_oferta { get; set; }
        public int id_vendedor { get; set; }
        public int id_comprador { get; set; }
        public int qtd_comprada { get; set; }
        public float valor_transacao { get; set; }
        public int avaliacao_vendedor { get; set; }
        public int avaliacao_comprador { get; set; }
        public string venda_status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }      
    }
}
