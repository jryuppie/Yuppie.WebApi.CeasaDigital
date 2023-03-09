using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao
{
    public class ProcessoNegociacaoModel
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public string StatusNegociacao { get; set; }
        public string SubStatusNegociacao { get; set; }
        public int QtdComprada { get; set; }
        public bool AprovacaoVendedor { get; set; }
        public bool AprovacaoComprador { get; set; }
        public bool AvisaInicioNegociacao { get; set; }
        public bool AvisaNegociacaoPendente { get; set; }
        public bool AvisaPropostaCancelada { get; set; }
        public bool AvisaCancelamento { get; set; }
        public bool AvisaConclusaoVenda { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime update_date { get; set; }
        public DateTime? DataEnvioMensagem { get; set; }
        public int EnvioMensagemContador { get; set; }
    }
}

