using System;
using System.ComponentModel.DataAnnotations;

namespace Yuppie.WebApi.Infra.Models.Negociacao
{
    public class OfertaModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdProduto { get; set; }
        public int IdUnMedida { get; set; }
        public int IdVendedor { get; set; }
        public float PesoUnMedida { get; set; }
        public int QtdDisponivel { get; set; }
        public bool Status { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public float ValorKg { get; set; }
        public float ValorUnMedida { get; set; }
        public DateTime? DataEnvioMensagem { get; set; }
        public int EnvioMensagemContador { get; set; }
    }
}


