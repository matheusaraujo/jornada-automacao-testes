using GestaoContratos.Dominio;
using GestaoContratos.Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio
{
    public class ContratoRepositorio : RepositorioBase
    {
        public IList<Contrato> ObterContratos()
        {
            var contratos = new List<Contrato>();
            string sql = "SELECT * FROM Contrato ORDER BY Ativo DESC, VolumeDisponivel DESC";

            using (var conexao = CriarConexao())
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
            var sql = @"INSERT INTO Contrato (VolumeDisponivel, DataInicioVigencia, DataFimVigencia, Ativo)
                VALUES (@VolumeDisponivel, @DataInicioVigencia, @DataFimVigencia, @Ativo); SELECT last_insert_rowid();";

            return ExecutarRetornoInteiro(sql,
                Parametro("VolumeDisponivel", contrato.VolumeDisponivel),
                Parametro("DataInicioVigencia", contrato.DataInicioVigencia.Ticks),
                Parametro("DataFimVigencia", contrato.DataFimVigencia.Ticks),
                Parametro("Ativo", contrato.Ativo)
            );
        }

        public Contrato ObterContrato(int contratoId)
        {
            string sql = "SELECT * FROM Contrato WHERE ContratoId = @ContratoId";
            return ExecutarConsultaUnica(sql, Conversor.CriarContrato, Parametro("ContratoId", contratoId));
        }

        public void EditarContrato(Contrato contrato)
        {
            string sql = @"UPDATE Contrato 
                SET VolumeDisponivel = @VolumeDisponivel, 
                DataInicioVigencia = @DataInicioVigencia, 
                DataFimVigencia = @DataFimVigencia, 
                Ativo = @Ativo
                WHERE ContratoId = @ContratoId";

            Executar(sql, Parametro("ContratoId", contrato.ContratoId),
                Parametro("VolumeDisponivel", contrato.VolumeDisponivel),
                Parametro("DataInicioVigencia", contrato.DataInicioVigencia.Ticks),
                Parametro("DataFimVigencia", contrato.DataFimVigencia.Ticks),
                Parametro("Ativo", contrato.Ativo)
            );
        }

        public void DeletarContrato(int contratoId)
        {
            string sql = @"DELETE FROM Contrato WHERE ContratoId = @ContratoId";

            using (var conexao = CriarConexao())
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

            Executar(sql,
                Parametro("ContratoId", contrato.ContratoId),
                Parametro("VolumeDisponivel", contrato.VolumeDisponivel)
            );
        }
    }
}
