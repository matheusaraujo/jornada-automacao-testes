using GestaoContratos.Dominio;
using GestaoContratos.Repositorio;
using GestaoContratos.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoContratos.Processo
{
    public class ContratoProcesso
    {
        public IList<Contrato> ObterContratos()
        {
            return new ContratoRepositorio().ObterContratos();
        }   

        public Contrato ObterContrato(int contratoId)
        {
            return new ContratoRepositorio().ObterContrato(contratoId);
        }

        public int InserirContrato(Contrato contrato)
        {
            if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataInicioVigenciaInvalida);
            else if (contrato.DataFimVigencia < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataFimVigenciaInvalida);
            else if (contrato.VolumeDisponivel < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalido);

            return new ContratoRepositorio().InserirContrato(contrato);
        }

        public bool EditarContrato(Contrato contrato)
        {
            var contratoRepositorio = new ContratoRepositorio();
            var pedidoRepositorio = new PedidoRepositorio();

            if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataInicioVigenciaInvalida);
            else if (contrato.DataFimVigencia < DateTime.Now.Date)
                throw new RegraNegocioException(TipoRegraNegocio.DataFimVigenciaInvalida);
            else if (contrato.VolumeDisponivel < 1)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalido);

            var contratoAtual = contratoRepositorio.ObterContrato(contrato.ContratoId);
            if (contratoAtual == null)
                return false;

            var pedidos = pedidoRepositorio.ObterPedidos(contrato.ContratoId);
            var volumePedidosPendentes = pedidos.Where(p => !p.Atendido).Sum(p => p.Volume);
            if (contrato.VolumeDisponivel < volumePedidosPendentes)
                throw new RegraNegocioException(TipoRegraNegocio.VolumeDisponivelInvalidoEdicao);

            contratoRepositorio.EditarContrato(contrato);

            return true;
        }

        public bool DeletarContrato(int contratoId)
        {
            var contratoRepositorio = new ContratoRepositorio();
            var pedidoRepositorio = new PedidoRepositorio();

            var contratoAtual = contratoRepositorio.ObterContrato(contratoId);
            if (contratoAtual == null)
                return false;

            var pedidos = pedidoRepositorio.ObterPedidos(contratoId);
            if (pedidos != null && pedidos.Count > 0)
                throw new RegraNegocioException(TipoRegraNegocio.ContratoPossuiPedidos);

            contratoRepositorio.DeletarContrato(contratoId);

            return true;
        }
    }
}
