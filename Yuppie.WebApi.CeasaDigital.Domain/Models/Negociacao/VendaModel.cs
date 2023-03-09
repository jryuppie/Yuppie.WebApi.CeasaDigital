using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Negociacao
{
    public class VendaModel
    {
        public int Id { get; set; }
        public int IdOferta { get; set; }
        public int IdVendedor { get; set; }
        public int IdComprador { get; set; }
        public int QtdComprada { get; set; }
        public float ValorTransacao { get; set; }
        public int AvaliacaoVendedor { get; set; }
        public int AvaliacaoComprador { get; set; }
        public string VendaStatus { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
