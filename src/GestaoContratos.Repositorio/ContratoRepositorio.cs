using GestaoContratos.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio
{
    public class ContratoRepositorio
    {
        public IList<Contrato> ObterContratos()
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

        public int InserirContrato(Contrato contrato)
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

        public Contrato ObterContrato(int contratoId)
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

        public void EditarContrato(Contrato contrato)
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

        public void DeletarContrato(int contratoId)
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

        public void EditarVolumeContrato(Contrato contrato)
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
    }
}
