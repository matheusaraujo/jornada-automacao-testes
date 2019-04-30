using GestaoContratos.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoContratos.Repository
{
    public static class Repositorio
    {
        #region Contratos

        public static IList<Contrato> ObterContratos()
        {
            var contratos = new List<Contrato>();
            string sql = "SELECT * FROM Contrato ORDER BY Ativo DESC, VolumeDisponivel DESC";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    using (var consulta = comando.ExecuteReader())
                    {
                        while (consulta.Read())
                        {
                            contratos.Add(new Contrato()
                            {
                                ContratoId = int.Parse(consulta["ContratoId"].ToString()),
                                VolumeDisponivel = float.Parse(consulta["VolumeDisponivel"].ToString()),
                                DataInicioVigencia = new DateTime(long.Parse(consulta["DataInicioVigencia"].ToString())),
                                DataFimVigencia = new DateTime(long.Parse(consulta["DataFimVigencia"].ToString())),
                                Ativo = (consulta["Ativo"].ToString() == "1")
                            });
                        }
                        return contratos;
                    }
                }
            }
        }

        public static int InserirContrato(Contrato contrato)
        {
            string sql = @"INSERT INTO Contrato (VolumeDisponivel, DataInicioVigencia, DataFimVigencia, Ativo)
                VALUES (@VolumeDisponivel, @DataInicioVigencia, @DataFimVigencia, @Ativo); SELECT last_insert_rowid();";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("VolumeDisponivel", contrato.VolumeDisponivel);
                    comando.Parameters.AddWithValue("DataInicioVigencia", contrato.DataInicioVigencia.Ticks);
                    comando.Parameters.AddWithValue("DataFimVigencia", contrato.DataFimVigencia.Ticks);
                    comando.Parameters.AddWithValue("Ativo", contrato.Ativo);
                    return int.Parse(comando.ExecuteScalar().ToString());
                }
            }
        }

        public static Contrato ObterContrato(int contratoId)
        {
            string sql = "SELECT * FROM Contrato WHERE ContratoId = @ContratoId";
            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    using (var consulta = comando.ExecuteReader())
                    {
                        if (!consulta.Read())
                            return null;

                        return new Contrato()
                        {
                            ContratoId = int.Parse(consulta["ContratoId"].ToString()),
                            VolumeDisponivel = float.Parse(consulta["VolumeDisponivel"].ToString()),
                            DataInicioVigencia = new DateTime(long.Parse(consulta["DataInicioVigencia"].ToString())),
                            DataFimVigencia = new DateTime(long.Parse(consulta["DataFimVigencia"].ToString())),
                            Ativo = (consulta["Ativo"].ToString() == "1")
                        };
                    }
                }
            }
        }

        public static void EditarContrato(Contrato contrato)
        {
            string sql = @"UPDATE Contrato 
                SET VolumeDisponivel = @VolumeDisponivel, 
                DataInicioVigencia = @DataInicioVigencia, 
                DataFimVigencia = @DataFimVigencia, 
                Ativo = @Ativo
                WHERE ContratoId = @ContratoId";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", contrato.ContratoId);
                    comando.Parameters.AddWithValue("VolumeDisponivel", contrato.VolumeDisponivel);
                    comando.Parameters.AddWithValue("DataInicioVigencia", contrato.DataInicioVigencia.Ticks);
                    comando.Parameters.AddWithValue("DataFimVigencia", contrato.DataFimVigencia.Ticks);
                    comando.Parameters.AddWithValue("Ativo", contrato.Ativo);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void DeletarContrato(int contratoId)
        {
            string sql = @"DELETE FROM Contrato WHERE ContratoId = @ContratoId";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void EditarVolumeContrato(Contrato contrato)
        {
            string sql = @"UPDATE Contrato 
                SET VolumeDisponivel = @VolumeDisponivel
                WHERE ContratoId = @ContratoId";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.Parameters.AddWithValue("ContratoId", contrato.ContratoId);
                    comando.Parameters.AddWithValue("VolumeDisponivel", contrato.VolumeDisponivel);
                    comando.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Pedidos

        public static IList<Pedido> ObterPedidos(int contratoId)
        {
            var lista = new List<Pedido>();

            string sql = @"SELECT * FROM Pedido 
                WHERE ContratoId = @ContratoId
                ORDER BY Atendido, DataPedido, Volume DESC";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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

        public static Pedido ObterPedido(int contratoId, int pedidoId)
        {
            string sql = "SELECT * FROM Pedido WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";
            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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

        public static int InserirPedido(Pedido pedido)
        {
            string sql = @"INSERT INTO Pedido (ContratoId, Volume, DataPedido, Atendido)
                    VALUES (@ContratoId, @Volume, @DataPedido, @Atendido); SELECT last_insert_rowid();";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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

        public static void EditarPedido(Pedido pedido)
        {
            string sql = @"UPDATE Pedido 
                SET Volume = @Volume,
                DataPedido = @DataPedido,
                Atendido = @Atendido
                WHERE ContratoId = @ContratoId AND PedidoId = @PedidoId";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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

        public static void DeletarPedido(int contratoId, int pedidoId)
        {
            string sql = @"DELETE FROM Pedido WHERE ContratoId = @ContratoId AND ContratoId = @ContratoId";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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

        #endregion

        #region Testes

        public static void IniciarTestes()
        {
            var sql = @"DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato';
                DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.ExecuteNonQuery();                    
                }
            }
        }

        #endregion
    }
}