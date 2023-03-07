using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Formulario
{
    public class VendaFormulario
    {
        public int idVenda { get; set; }
        public int idUsuario { get; set; }
    }

    public class CadastrarVendaFormulario
    {
        public int idOferta { get; set; }
        public int idComprador { get; set; }
        public int qtd { get; set; }      
    }


    public class EditarVendaFormulario
    {
        public int idVenda { get; set; }        
        public int qtd { get; set; }
    }
}
