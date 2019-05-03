using GestaoContratos.Dominio;
using GestaoContratos.Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio
{
    public class PedidoRepositorio : RepositorioBase
    {
        public IList<Pedido> ObterPedidos(int contratoId)
        {
            var lista = new List<Pedido>();

            string sql = @"SELECT * FROM Pedido 
                WHERE ContratoId = @ContratoId
                ORDER BY Atendido, DataPedido, Volume DESC";

            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    comando.Connection.Open();
                    using (var consulta = comando.ExecuteReader())
                    {
                        while (consulta.Read())
                        {
                            lista.Add(new Pedido()
                            {
                                PedidoId = int.Parse(consulta["PedidoId"].ToString()),
                                ContratoId = int.Parse(consulta["ContratoId"].ToString()),
                                Volume = float.Parse(consulta["Volume"].ToString()),
                                DataPedido = new DateTime(long.Parse(consulta["DataPedido"].ToString())),
                                Atendido = (consulta["Atendido"].ToString() == "1")
                            });
                        }
                        return lista;
                    }
                }
            }
        }

        public Pedido ObterPedido(int contratoId, int pedidoId)
        {
            string sql = "SELECT * FROM Pedido WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";
            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    comando.Parameters.AddWithValue("PedidoId", pedidoId);
                    comando.Connection.Open();

                    using (var consulta = comando.ExecuteReader())
                    {
                        if (!consulta.Read())
                            return null;

                        return new Pedido()
                        {
                            PedidoId = int.Parse(consulta["PedidoId"].ToString()),
                            ContratoId = int.Parse(consulta["ContratoId"].ToString()),
                            Volume = float.Parse(consulta["Volume"].ToString()),
                            DataPedido = new DateTime(long.Parse(consulta["DataPedido"].ToString())),
                            Atendido = (consulta["Atendido"].ToString() == "1"),
                        };
                    }
                }
            }
        }

        public int InserirPedido(Pedido pedido)
        {
            string sql = @"INSERT INTO Pedido (ContratoId, Volume, DataPedido, Atendido)
                    VALUES (@ContratoId, @Volume, @DataPedido, @Atendido); SELECT last_insert_rowid();";

            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", pedido.ContratoId);
                    comando.Parameters.AddWithValue("Volume", pedido.Volume);
                    comando.Parameters.AddWithValue("DataPedido", pedido.DataPedido.Ticks);
                    comando.Parameters.AddWithValue("Atendido", pedido.Atendido ? 1 : 0);
                    return int.Parse(comando.ExecuteScalar().ToString());
                }
            }
        }

        public void EditarPedido(Pedido pedido)
        {
            string sql = @"UPDATE Pedido 
                SET Volume = @Volume,
                DataPedido = @DataPedido,
                Atendido = @Atendido
                WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";

            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", pedido.ContratoId);
                    comando.Parameters.AddWithValue("PedidoId", pedido.PedidoId);
                    comando.Parameters.AddWithValue("Volume", pedido.Volume);
                    comando.Parameters.AddWithValue("DataPedido", pedido.DataPedido.Ticks);
                    comando.Parameters.AddWithValue("Atendido", pedido.Atendido);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DeletarPedido(int contratoId, int pedidoId)
        {
            string sql = @"DELETE FROM Pedido WHERE ContratoId = @ContratoId AND ContratoId = @ContratoId";

            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    comando.Parameters.AddWithValue("PedidoId", pedidoId);
                    comando.ExecuteNonQuery();
                    comando.Connection.Close();
                }
            }
        }
    }
}
