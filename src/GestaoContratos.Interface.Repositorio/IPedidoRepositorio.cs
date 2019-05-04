using GestaoContratos.Dominio.Entidade;
using System.Collections.Generic;

namespace GestaoContratos.Interface.Repositorio
{
    public interface IPedidoRepositorio
    {
        IList<Pedido> ObterPedidos(int contratoId);
        Pedido ObterPedido(int contratoId, int pedidoId);
        int InserirPedido(Pedido pedido);
        void EditarPedido(Pedido pedido);
        void DeletarPedido(int contratoId, int pedidoId);
    }
}
