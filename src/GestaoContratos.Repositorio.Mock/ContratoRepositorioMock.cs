using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    public class ContratoRepositorioMock : IContratoRepositorio
    {
        public void DeletarContrato(int contratoId)
        {
            BancoMemoria.Contratos.RemoveAll(c => c.ContratoId == contratoId);
        }

        public void EditarContrato(Contrato contrato)
        {
            BancoMemoria.Contratos.RemoveAll(c => c.ContratoId == contrato.ContratoId);
            BancoMemoria.Contratos.Add(contrato);
        }

        public void EditarVolumeContrato(Contrato contrato)
        {   
            EditarContrato(contrato);
        }

        public int InserirContrato(Contrato contrato)
        {
            contrato.ContratoId = BancoMemoria.Contratos.Count + 1;
            BancoMemoria.Contratos.Add(contrato);
            return contrato.ContratoId;
        }

        public Contrato ObterContrato(int contratoId)
        {
            return BancoMemoria.Contratos.Find(c => c.ContratoId == contratoId);
        }

        public IList<Contrato> ObterContratos()
        {
            return BancoMemoria.Contratos;
        }
    }
}
