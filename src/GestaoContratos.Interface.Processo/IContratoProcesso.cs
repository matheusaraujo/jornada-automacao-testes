using GestaoContratos.Dominio.Dto;
using System.Collections.Generic;

namespace GestaoContratos.Interface.Processo
{
    public interface IContratoProcesso
    {
        IList<ContratoDto> ObterContratos();
        ContratoDto ObterContrato(int contratoId);
        int InserirContrato(ContratoDto contratoDto);
        bool EditarContrato(ContratoDto contratoDto);
        bool DeletarContrato(int contratoId);
    }
}
