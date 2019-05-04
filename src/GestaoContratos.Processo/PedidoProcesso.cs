using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Util;
using GestaoPedidos.Processo.Mapeador;
using System;
using System.Collections.Generic;

namespace GestaoContratos.Processo
{
    public class PedidoProcesso : IPedidoProcesso
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IContratoRepositorio _contratoRepositorio;

        public PedidoProcesso(IPedidoRepositorio pedidoRepositorio, IContratoRepositorio contratoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _contratoRepositorio = contratoRepositorio;
        }

        public IList<PedidoDto> ObterPedidos(int contratoId)
        {
            return _pedidoRepositorio.ObterPedidos(contratoId).Converter();
        }

        public PedidoDto ObterPedido(int contratoId, int pedidoId)
        {
            return _pedidoRepositorio.ObterPedido(contratoId, pedidoId).Converter();
        }

        public int InserirPedido(PedidoDto pedidoDto)
        {
            var pedido = pedidoDto.Converter();

            if (pedido.Volume < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumePedidoInvalido);
            else if (pedido.DataPedido < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoInvalida);
            else if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoInsercao);

            var contrato = _contratoRepositorio.ObterContrato(pedido.ContratoId);
            if (contrato == null)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);
            else if (contrato.VolumeDisponivel < pedido.Volume)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeContratoInsuficiente);
            else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);
            else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);

            int pedidoId = _pedidoRepositorio.InserirPedido(pedido);

            contrato.VolumeDisponivel -= pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return pedidoId;
        }

        public bool EditarPedido(PedidoDto pedidoDto)
        {

            var pedido = pedidoDto.Converter();

            if (pedido.Volume < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumePedidoInvalido);
            else if (pedido.DataPedido < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoInvalida);

            var contrato = _contratoRepositorio.ObterContrato(pedido.ContratoId);
            if (contrato == null)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);
            else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);
            else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);

            var pedidoAtual = _pedidoRepositorio.ObterPedido(pedido.ContratoId, pedido.PedidoId);

            if (pedidoAtual == null)
                return false;
            else if (pedidoAtual.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoEdicao);
            else if (pedido.Volume > contrato.VolumeDisponivel + pedidoAtual.Volume)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeContratoInsuficiente);

            _pedidoRepositorio.EditarPedido(pedido);
            contrato.VolumeDisponivel = contrato.VolumeDisponivel + pedidoAtual.Volume - pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }

        public bool DeletarPedido(int contratoId, int pedidoId)
        {
            var pedido = _pedidoRepositorio.ObterPedido(contratoId, pedidoId);

            if (pedido == null)
                return false;
            else if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoExclusao);

            var contrato = _contratoRepositorio.ObterContrato(contratoId);
            if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);

            _pedidoRepositorio.DeletarPedido(contratoId, pedidoId);

            contrato.VolumeDisponivel += pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }

        public bool AtenderPedido(int contratoId, int pedidoId)
        {
            var pedido = _pedidoRepositorio.ObterPedido(contratoId, pedidoId);
            if (pedido == null)
                return false;
            else if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoEdicao);

            var contrato = _contratoRepositorio.ObterContrato(contratoId);
            if (contrato == null)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);

            pedido.Atendido = true;
            _pedidoRepositorio.EditarPedido(pedido);

            return true;
        }
    }
}
