using GestaoContratos.Dominio.Dto;
using System.Collections.Generic;

namespace GestaoContratos.Interface.Processo
{
    public interface IPedidoProcesso
    {
        IList<PedidoDto> ObterPedidos(int contratoId);
        PedidoDto ObterPedido(int contratoId, int pedidoId);
        int InserirPedido(PedidoDto pedidoDto);
        bool EditarPedido(PedidoDto pedidoDto);
        bool DeletarPedido(int contratoId, int pedidoId);
        bool AtenderPedido(int contratoId, int pedidoId);
    }
}
