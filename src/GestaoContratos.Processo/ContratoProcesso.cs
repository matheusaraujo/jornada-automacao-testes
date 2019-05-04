using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Processo.Mapeador;
using GestaoContratos.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoContratos.Processo
{
    public class ContratoProcesso : IContratoProcesso
    {
        private readonly IContratoRepositorio _contratoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public ContratoProcesso(IContratoRepositorio contratoRepositorio, IPedidoRepositorio pedidoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IList<ContratoDto> ObterContratos()
        {
            return _contratoRepositorio.ObterContratos().Converter();
        }   

        public ContratoDto ObterContrato(int contratoId)
        {
            return _contratoRepositorio.ObterContrato(contratoId).Converter();
        }

        public int InserirContrato(ContratoDto contratoDto)
        {
            var contrato = contratoDto.Converter();
            if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataInicioVigenciaInvalida);
            else if (contrato.DataFimVigencia < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataFimVigenciaInvalida);
            else if (contrato.VolumeDisponivel < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalido);

            return _contratoRepositorio.InserirContrato(contrato);
        }

        public bool EditarContrato(ContratoDto contratoDto)
        {
            var contrato = contratoDto.Converter();
            
            if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataInicioVigenciaInvalida);
            else if (contrato.DataFimVigencia < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataFimVigenciaInvalida);
            else if (contrato.VolumeDisponivel < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalido);

            var contratoAtual = _contratoRepositorio.ObterContrato(contrato.ContratoId);
            if (contratoAtual == null)
                return false;

            var pedidos = _pedidoRepositorio.ObterPedidos(contrato.ContratoId);
            var volumePedidosPendentes = pedidos.Where(p => !p.Atendido).Sum(p => p.Volume);
            if (contrato.VolumeDisponivel < volumePedidosPendentes)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalidoEdicao);

            _contratoRepositorio.EditarContrato(contrato);

            return true;
        }

        public bool DeletarContrato(int contratoId)
        {
            var contratoAtual = _contratoRepositorio.ObterContrato(contratoId);
            if (contratoAtual == null)
                return false;

            var pedidos = _pedidoRepositorio.ObterPedidos(contratoId);
            if (pedidos != null && pedidos.Count > 0)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoPossuiPedidos);

            _contratoRepositorio.DeletarContrato(contratoId);

            return true;
        }
    }
}
