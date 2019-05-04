using GestaoContratos.Dominio.Entidade;
using System.Collections.Generic;

namespace GestaoContratos.Interface.Repositorio
{
    public interface IContratoRepositorio
    {
        IList<Contrato> ObterContratos();
        int InserirContrato(Contrato contrato);
        Contrato ObterContrato(int contratoId);
        void EditarContrato(Contrato contrato);
        void DeletarContrato(int contratoId);
        void EditarVolumeContrato(Contrato contrato);
    }
}
