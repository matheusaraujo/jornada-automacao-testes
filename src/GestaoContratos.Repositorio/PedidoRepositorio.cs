using GestaoContratos.Dominio;
using GestaoContratos.Repositorio.Base;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio
{
    public class PedidoRepositorio : RepositorioBase
    {
        public IList<Pedido> ObterPedidos(int contratoId)
        {
            string sql = @"SELECT * FROM Pedido 
                WHERE ContratoId = @ContratoId
                ORDER BY Atendido, DataPedido, Volume DESC";
            return ExecutarConsulta(sql, Conversor.CriarPedido, Parametro("ContratoId", contratoId));
        }

        public Pedido ObterPedido(int contratoId, int pedidoId)
        {
            string sql = "SELECT * FROM Pedido WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";
            return ExecutarConsultaUnica(sql, Conversor.CriarPedido, Parametro("ContratoId", contratoId), Parametro("PedidoId", pedidoId));
        }

        public int InserirPedido(Pedido pedido)
        {
            string sql = @"INSERT INTO Pedido (ContratoId, Volume, DataPedido, Atendido)
                    VALUES (@ContratoId, @Volume, @DataPedido, @Atendido); SELECT last_insert_rowid();";

            return ExecutarRetornoInteiro(sql, Parametro("ContratoId", pedido.ContratoId),
                Parametro("Volume", pedido.Volume),
                Parametro("DataPedido", pedido.DataPedido.Ticks),
                Parametro("Atendido", 0)
            );
        }

        public void EditarPedido(Pedido pedido)
        {
            string sql = @"UPDATE Pedido 
                SET Volume = @Volume,
                DataPedido = @DataPedido,
                Atendido = @Atendido
                WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";

            Executar(sql, Parametro("ContratoId", pedido.ContratoId),
                Parametro("PedidoId", pedido.PedidoId),
                Parametro("Volume", pedido.Volume),
                Parametro("DataPedido", pedido.DataPedido.Ticks),
                Parametro("Atendido", pedido.Atendido)
            );
        }

        public void DeletarPedido(int contratoId, int pedidoId)
        {
            string sql = @"DELETE FROM Pedido WHERE ContratoId = @ContratoId AND ContratoId = @ContratoId";
            Executar(sql, Parametro("ContratoId", contratoId), Parametro("PedidoId", pedidoId));
        }
    }
}
