using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    public class PedidoRepositorioMock : IPedidoRepositorio
    {   
        public void DeletarPedido(int contratoId, int pedidoId)
        {
            BancoMemoria.Pedidos.RemoveAll(p => p.ContratoId == contratoId && p.PedidoId == pedidoId);
        }

        public void EditarPedido(Pedido pedido)
        {
            BancoMemoria.Pedidos.RemoveAll(p => p.ContratoId == pedido.ContratoId && p.PedidoId == pedido.PedidoId);
            BancoMemoria.Pedidos.Add(pedido);
        }

        public int InserirPedido(Pedido pedido)
        {
            pedido.PedidoId = BancoMemoria.Pedidos.Count + 1;
            BancoMemoria.Pedidos.Add(pedido);
            return pedido.PedidoId;
        }

        public Pedido ObterPedido(int contratoId, int pedidoId)
        {
            return BancoMemoria.Pedidos.Find(p => p.ContratoId == contratoId && p.PedidoId == pedidoId);
        }

        public IList<Pedido> ObterPedidos(int contratoId)
        {
            return BancoMemoria.Pedidos.FindAll(p => p.ContratoId == contratoId);
        }
    }
}
