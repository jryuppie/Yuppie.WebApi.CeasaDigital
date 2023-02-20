using System;
using System.Collections.Generic;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.Enums
{
    public enum NegociacaoStatusResponse
    {
        Andamento,
        Concluido,
        Cancelado,
        ConcluidoAnteriormente,
        ConcluidoPendenteComprador,
        ConcluidoPendenteVendedor,
        DesativadoPendenteComprador,
        DesativadoPendenteVendedor,
        Desativado,
        Atualizado,
        ErroProcessamento,
        Existente,
        Iniciada,
        Inexistente

    }

    public static class NegociacaoStatusResponseExtensions
    {
        public static string PegarDescricao(this NegociacaoStatusResponse status)
        {
            switch (status)
            {
                case NegociacaoStatusResponse.Andamento:
                    return "Negociação em andamento.";
                case NegociacaoStatusResponse.Concluido:
                    return "Negociação concluída.";
                case NegociacaoStatusResponse.Cancelado:
                    return "Negociação cancelada.";
                case NegociacaoStatusResponse.ConcluidoAnteriormente:
                    return "Operação Inválida: A negociação está com o status CONCLUIDO.";
                case NegociacaoStatusResponse.ConcluidoPendenteComprador:
                    return "Negociação concluída pendente de aprovação do comprador.";
                case NegociacaoStatusResponse.ConcluidoPendenteVendedor:
                    return "Negociação concluída pendente de aprovação do vendedor.";
                case NegociacaoStatusResponse.DesativadoPendenteComprador:
                    return "Negociação desativada pendente de aprovação do comprador.";
                case NegociacaoStatusResponse.DesativadoPendenteVendedor:
                    return "Negociação desativada pendente de aprovação do vendedor.";
                case NegociacaoStatusResponse.Desativado:
                    return "Negociação desativada.";
                case NegociacaoStatusResponse.Atualizado:
                    return "Negociação atualizada.";
                case NegociacaoStatusResponse.ErroProcessamento:
                    return "Erro de processamento. Tente Novamente!";
                case NegociacaoStatusResponse.Existente:
                    return "Negociação existente.";
                case NegociacaoStatusResponse.Iniciada:
                    return "Negociação iniciada.";
                case NegociacaoStatusResponse.Inexistente:
                    return "Negociação iniciada.";
                default:
                    return "";
            }
        }

        public static int PegarCodigoStatus(this NegociacaoStatusResponse status)
        {
            switch (status)
            {
                case NegociacaoStatusResponse.Andamento:
                    return 200;
                case NegociacaoStatusResponse.Concluido:
                    return 200;
                case NegociacaoStatusResponse.Cancelado:
                    return 200;
                case NegociacaoStatusResponse.ConcluidoAnteriormente:
                    return 200;
                case NegociacaoStatusResponse.ConcluidoPendenteComprador:
                    return 200;
                case NegociacaoStatusResponse.ConcluidoPendenteVendedor:
                    return 200;
                case NegociacaoStatusResponse.DesativadoPendenteComprador:
                    return 200;
                case NegociacaoStatusResponse.DesativadoPendenteVendedor:
                    return 200;
                case NegociacaoStatusResponse.Desativado:
                    return 200;
                case NegociacaoStatusResponse.Atualizado:
                    return 200;
                case NegociacaoStatusResponse.ErroProcessamento:
                    return 503;
                case NegociacaoStatusResponse.Existente:
                    return 422;
                case NegociacaoStatusResponse.Iniciada:
                    return 200;
                case NegociacaoStatusResponse.Inexistente:
                    return 422;
                default:
                    return 200;
            }
        }
    }
}
