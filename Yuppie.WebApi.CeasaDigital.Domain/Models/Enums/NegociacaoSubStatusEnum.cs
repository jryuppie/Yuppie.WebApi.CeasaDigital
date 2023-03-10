using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Enums
{
    public enum NegociacaoSubStatusEnum
    {
        AguardandoAprovacaoVendedorCancelamento,
        AguardandoAprovacaoCompradorCancelamento,
        AguardandoAprovacaoConclusaoVendedor,
        AgardandoAprovacaoConclusaoComprador,
        AguardandoAprovacaoAlteracao,
        Concluido,
        Cancelado
    }

    public static class NegociacaoSubStatusEnumExtensions
    {
        public static string BuscarDescricao(this NegociacaoSubStatusEnum status)
        {
            switch (status)
            {
                case NegociacaoSubStatusEnum.AguardandoAprovacaoVendedorCancelamento:
                    return "Aguardando vendedor aprovar cancelamento";                  
                case NegociacaoSubStatusEnum.AguardandoAprovacaoCompradorCancelamento:
                    return "Aguardando comprador aprovar cancelamento";
                case NegociacaoSubStatusEnum.AguardandoAprovacaoConclusaoVendedor:
                    return "Aguardando vendedor aprovar conclusão";
                case NegociacaoSubStatusEnum.AgardandoAprovacaoConclusaoComprador:
                    return "Aguardando comprador aprovar conclusão";
                case NegociacaoSubStatusEnum.AguardandoAprovacaoAlteracao:
                    return "Aguardando aprovação da alteração de peso";
                case NegociacaoSubStatusEnum.Concluido:
                    return "CONCLUIDO";
                case NegociacaoSubStatusEnum.Cancelado:
                    return "CANCELADO";
                default:
                    return "";
            }
        }        
    }
}
