using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Enums
{
    public enum NegociacaoStatus
    {
        Andamento,
        Concluido,
        Cancelado 
    }

    public static class NegociacaoStatusString
    {
        public static string Andamento = "PROCESSO";
        public static string Concluido = "CONCLUIDO";
        public static string Cancelado = "CANCELADO";
    }

}
