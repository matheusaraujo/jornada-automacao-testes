using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    public class ContratoRepositorioMock : IContratoRepositorio
    {
        public void DeletarContrato(int contratoId)
        {
            BancoMemoria.contratos.RemoveAll(c => c.ContratoId == contratoId);
        }

        public void EditarContrato(Contrato contrato)
        {
            BancoMemoria.contratos.RemoveAll(c => c.ContratoId == contrato.ContratoId);
            BancoMemoria.contratos.Add(contrato);
        }

        public void EditarVolumeContrato(Contrato contrato)
        {
            BancoMemoria.contratos.RemoveAll(c => c.ContratoId == contrato.ContratoId);
            BancoMemoria.contratos.Add(contrato);
        }

        public int InserirContrato(Contrato contrato)
        {
            contrato.ContratoId = BancoMemoria.contratos.Count + 1;
            BancoMemoria.contratos.Add(contrato);
            return contrato.ContratoId;
        }

        public Contrato ObterContrato(int contratoId)
        {
            return BancoMemoria.contratos.Find(c => c.ContratoId == contratoId);
        }

        public IList<Contrato> ObterContratos()
        {
            return BancoMemoria.contratos;
        }
    }
}
