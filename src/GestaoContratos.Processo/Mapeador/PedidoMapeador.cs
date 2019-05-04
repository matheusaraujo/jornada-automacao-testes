using GestaoContratos.Dominio.Dto;
using GestaoContratos.Dominio.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace GestaoPedidos.Processo.Mapeador
{
    public static class PedidoMapeador
    {
        public static PedidoDto Converter(this Pedido entidade)
        {
            return entidade == null ? null : new PedidoDto()
            {
                PedidoId = entidade.PedidoId,
                ContratoId = entidade.ContratoId,
                DataPedido = entidade.DataPedido,
                Volume = entidade.Volume,
                Atendido = entidade.Atendido
            };
        }

        public static Pedido Converter(this PedidoDto dto)
        {
            return dto == null ? null : new Pedido()
            {
                PedidoId = dto.PedidoId,
                ContratoId = dto.ContratoId,
                DataPedido = dto.DataPedido,
                Volume = dto.Volume,
                Atendido = dto.Atendido
            };
        }

        public static IList<PedidoDto> Converter(this IList<Pedido> entidades)
        {
            return entidades.ToList().ConvertAll(e => e.Converter());
        }

        public static IList<Pedido> Converter(this IList<PedidoDto> entidades)
        {
            return entidades.ToList().ConvertAll(e => e.Converter());
        }
    }
}
