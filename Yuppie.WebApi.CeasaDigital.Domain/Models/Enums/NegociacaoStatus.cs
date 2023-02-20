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

    public static class NegociacaoStatusExtensions
    {
        public static string PegarDescricao(this NegociacaoStatus status)
        {
            switch (status)
            {
                case NegociacaoStatus.Andamento:
                    return "PROCESSO";                    
                case NegociacaoStatus.Concluido:
                    return "CONCLUIDO";
                case NegociacaoStatus.Cancelado:
                    return "CANCELADO";
                default:
                    return "";
            }
        }
    }
}
