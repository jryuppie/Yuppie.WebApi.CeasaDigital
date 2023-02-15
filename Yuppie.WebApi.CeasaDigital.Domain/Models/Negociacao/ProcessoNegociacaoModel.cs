using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao
{
    public class ProcessoNegociacaoModel
    {
        public int id { get; set; }
        public int id_venda { get; set; }
        public string status_negociacao { get; set; }
        public string sub_status_negociacao { get; set; }
        public int qtd_comprada { get; set; }
        public bool aprovacao_vendedor { get; set; }
        public bool aprovacao_comprador { get; set; }
        public bool avisa_inicio_negociacao { get; set; }
        public bool avisa_negociacao_pendente { get; set; }
        public bool avisa_proposta_cancelamento { get; set; }
        public bool avisa_cancelamento { get; set; }
        public bool avisa_conclusao_venda { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
    }
}

