using GestaoContratos.Dominio.Dto;
using GestaoContratos.Dominio.Entidade;
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

        public PedidoProcesso(IContratoRepositorio contratoRepositorio, IPedidoRepositorio pedidoRepositorio)
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

        public void ValidarPedido(Pedido pedido, Contrato contrato, DateTime hoje)
        {
            if (pedido.Volume < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumePedidoInvalido);
            else if (pedido.DataPedido < hoje)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoInvalida);
            else if (contrato == null)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);
            else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);
            else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataPedidoForaVigenciaContrato);
        }

        public void ValidarPedidoInsercao(Pedido pedido, Contrato contrato, DateTime hoje)
        {
            ValidarPedido(pedido, contrato, hoje);
            if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoInsercao);
            else if (contrato.VolumeDisponivel < pedido.Volume)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeContratoInsuficiente);
        }

        public void ValidarPedidoEdicao(Pedido pedido, Contrato contrato, Pedido pedidoAtual, DateTime hoje)
        {
            ValidarPedido(pedido, contrato, hoje);            
            if (pedidoAtual.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoEdicao);
            else if (pedido.Volume > contrato.VolumeDisponivel + pedidoAtual.Volume)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeContratoInsuficiente);
        }

        public void ValidarPedidoDelecao(Pedido pedido, Contrato contrato)
        {
            if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoExclusao);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);
        }

        public void ValidarPedidoAtendimento(Pedido pedido, Contrato contrato)
        {
            if (pedido.Atendido)
                throw new RegraNegocioException(TipoRegraNegocio.StatusPedidoInvalidoEdicao);
                        
            if (contrato == null)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoInativo);
        }

        public int InserirPedido(PedidoDto pedidoDto)
        {
            var pedido = pedidoDto.Converter();
            var contrato = _contratoRepositorio.ObterContrato(pedido.ContratoId);
            ValidarPedidoInsercao(pedido, contrato, DateTime.Now.Date);

            int pedidoId = _pedidoRepositorio.InserirPedido(pedido);
            contrato.VolumeDisponivel -= pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return pedidoId;
        }

        public bool EditarPedido(PedidoDto pedidoDto)
        {

            var pedido = pedidoDto.Converter();
            var contrato = _contratoRepositorio.ObterContrato(pedido.ContratoId);
            var pedidoAtual = _pedidoRepositorio.ObterPedido(pedido.ContratoId, pedido.PedidoId);
            if (pedidoAtual == null)
                return false;

            ValidarPedidoEdicao(pedido, contrato, pedidoAtual, DateTime.Now.Date);

            _pedidoRepositorio.EditarPedido(pedido);
            contrato.VolumeDisponivel = contrato.VolumeDisponivel + pedidoAtual.Volume - pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }
        
        public bool DeletarPedido(int contratoId, int pedidoId)
        {
            var pedido = _pedidoRepositorio.ObterPedido(contratoId, pedidoId);
            var contrato = _contratoRepositorio.ObterContrato(contratoId);
            if (pedido == null)
                return false;

            ValidarPedidoDelecao(pedido, contrato);
            
            _pedidoRepositorio.DeletarPedido(contratoId, pedidoId);

            contrato.VolumeDisponivel += pedido.Volume;
            _contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }

        public bool AtenderPedido(int contratoId, int pedidoId)
        {
            var pedido = _pedidoRepositorio.ObterPedido(contratoId, pedidoId);
            var contrato = _contratoRepositorio.ObterContrato(contratoId);
            if (pedido == null)
                return false;

            ValidarPedidoAtendimento(pedido, contrato);

            pedido.Atendido = true;
            _pedidoRepositorio.EditarPedido(pedido);

            return true;
        }
    }
}
