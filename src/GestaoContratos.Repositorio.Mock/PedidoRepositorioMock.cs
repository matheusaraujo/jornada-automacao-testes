using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    public class PedidoRepositorioMock : IPedidoRepositorio
    {   
        public void DeletarPedido(int contratoId, int pedidoId)
        {
            BancoMemoria.pedidos.RemoveAll(p => p.ContratoId == contratoId && p.PedidoId == pedidoId);
        }

        public void EditarPedido(Pedido pedido)
        {
            BancoMemoria.pedidos.RemoveAll(p => p.ContratoId == pedido.ContratoId && p.PedidoId == pedido.PedidoId);
            BancoMemoria.pedidos.Add(pedido);
        }

        public int InserirPedido(Pedido pedido)
        {
            pedido.PedidoId = BancoMemoria.pedidos.Count + 1;
            BancoMemoria.pedidos.Add(pedido);
            return pedido.PedidoId;
        }

        public Pedido ObterPedido(int contratoId, int pedidoId)
        {
            return BancoMemoria.pedidos.Find(p => p.ContratoId == contratoId && p.PedidoId == pedidoId);
        }

        public IList<Pedido> ObterPedidos(int contratoId)
        {
            return BancoMemoria.pedidos.FindAll(p => p.ContratoId == contratoId);
        }
    }
}
