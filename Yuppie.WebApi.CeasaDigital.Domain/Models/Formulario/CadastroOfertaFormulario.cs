using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario
{
    public  class CadastroOfertaFormulario
    {
        //public CadastroOfertaFormulario(int idProduto, int idUnMedida, int idVendedor, int qtdDisponivel, float pesoUnMedida, float vlUnMedida)
        //{
        //    IdProduto = idProduto;
        //    IdUnMedida = idUnMedida;
        //    IdVendedor = idVendedor;
        //    QtdDisponivel = qtdDisponivel;
        //    PesoUnMedida = pesoUnMedida;
        //    VlUnMedida = vlUnMedida;
        //}

        public int IdProduto { get; set; }
        public int IdUnMedida { get; set; }
        public int IdVendedor { get; set; }
        public int QtdDisponivel { get; set; }
        public float PesoUnMedida { get; set; }
        public float VlUnMedida { get; set; }
    }
}

