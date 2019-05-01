﻿using GestaoContratos.Dominio;
using GestaoContratos.Repositorio;
using GestaoContratos.Util;
using System;
using System.Collections.Generic;

namespace GestaoContratos.Processo
{
    public class PedidoProcesso
    {
        public IList<Pedido> ObterPedidos(int contratoId)
        {
            return new PedidoRepositorio().ObterPedidos(contratoId);
        }

        public Pedido ObterPedido(int contratoId, int pedidoId)
        {
            return new PedidoRepositorio().ObterPedido(contratoId, pedidoId);
        }

        public int InserirPedido(Pedido pedido)
        {
            var contratoRepositorio = new ContratoRepositorio();
            var pedidoRepositorio = new PedidoRepositorio();

            if (pedido.Volume < 1)
                throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
            else if (pedido.DataPedido < DateTime.Now.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);
            else if (pedido.Atendido)
                throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoInsercao);

            var contrato = contratoRepositorio.ObterContrato(pedido.ContratoId);
            if (contrato == null)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
            else if (contrato.VolumeDisponivel < pedido.Volume)
                throw new RegraNegocioException(RegraNegocioEnum.VolumeContratoInsuficiente);
            else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
            else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

            int pedidoId = pedidoRepositorio.InserirPedido(pedido);

            contrato.VolumeDisponivel -= pedido.Volume;
            contratoRepositorio.EditarVolumeContrato(contrato);

            return pedidoId;
        }

        public bool EditarPedido(Pedido pedido)
        {
            var pedidoRepositorio = new PedidoRepositorio();
            var contratoRepositorio = new ContratoRepositorio();

            if (pedido.Volume < 1)
                throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
            else if (pedido.DataPedido < DateTime.Now.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);

            var contrato = contratoRepositorio.ObterContrato(pedido.ContratoId);
            if (contrato == null)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
            else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
            else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

            var pedidoAtual = pedidoRepositorio.ObterPedido(pedido.ContratoId, pedido.PedidoId);

            if (pedidoAtual == null)
                return false;
            else if (pedidoAtual.Atendido)
                throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoEdicao);
            else if (pedido.Volume > contrato.VolumeDisponivel + pedidoAtual.Volume)
                throw new RegraNegocioException(RegraNegocioEnum.VolumeContratoInsuficiente);

            pedidoRepositorio.EditarPedido(pedido);
            contrato.VolumeDisponivel = contrato.VolumeDisponivel + pedidoAtual.Volume - pedido.Volume;
            contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }

        public bool DeletarPedido(int contratoId, int pedidoId)
        {
            var pedidoRepositorio = new PedidoRepositorio();
            var contratoRepositorio = new ContratoRepositorio();

            var pedido = pedidoRepositorio.ObterPedido(contratoId, pedidoId);

            if (pedido == null)
                return false;
            else if (pedido.Atendido)
                throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoExclusao);

            var contrato = contratoRepositorio.ObterContrato(contratoId);
            if (!contrato.Ativo)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);

            pedidoRepositorio.DeletarPedido(contratoId, pedidoId);

            contrato.VolumeDisponivel += pedido.Volume;
            contratoRepositorio.EditarVolumeContrato(contrato);

            return true;
        }

        public bool AtenderPedido(int contratoId, int pedidoId)
        {
            var pedidoRepositorio = new PedidoRepositorio();
            var contratoRepositorio = new ContratoRepositorio();

            var pedido = pedidoRepositorio.ObterPedido(contratoId, pedidoId);
            if (pedido == null)
                return false;
            else if (pedido.Atendido)
                throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoEdicao);

            var contrato = contratoRepositorio.ObterContrato(contratoId);
            if (contrato == null)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
            else if (!contrato.Ativo)
                throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);

            pedido.Atendido = true;
            pedidoRepositorio.EditarPedido(pedido);

            return true;
        }
    }
}
