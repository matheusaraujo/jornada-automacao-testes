using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Repositorio.Base;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio
{
    public class ContratoRepositorio : RepositorioBase, IContratoRepositorio
    {
        public IList<Contrato> ObterContratos()
        {   
            string sql = "SELECT * FROM Contrato ORDER BY Ativo DESC, VolumeDisponivel DESC";
            return ExecutarConsulta(sql, Conversor.CriarContrato);            
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
            Executar(sql, Parametro("ContratoId", contratoId));
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
