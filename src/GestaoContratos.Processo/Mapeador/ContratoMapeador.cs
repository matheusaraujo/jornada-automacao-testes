using GestaoContratos.Dominio.Dto;
using GestaoContratos.Dominio.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace GestaoContratos.Processo.Mapeador
{
    public static class ContratoMapeador
    {
        public static ContratoDto Converter(this Contrato entidade)
        {
            return entidade == null ? null : new ContratoDto()
            {
                ContratoId = entidade.ContratoId,
                DataInicioVigencia = entidade.DataInicioVigencia,
                DataFimVigencia = entidade.DataFimVigencia,
                VolumeDisponivel = entidade.VolumeDisponivel,
                Ativo = entidade.Ativo
            };
        }

        public static Contrato Converter(this ContratoDto dto)
        {
            return dto == null ? null : new Contrato()
            {
                ContratoId = dto.ContratoId,
                DataInicioVigencia = dto.DataInicioVigencia,
                DataFimVigencia = dto.DataFimVigencia,
                VolumeDisponivel = dto.VolumeDisponivel,
                Ativo = dto.Ativo
            };
        }

        public static IList<ContratoDto> Converter(this IList<Contrato> entidades)
        {
            return entidades.ToList().ConvertAll(e => e.Converter());
        }
    }
}
