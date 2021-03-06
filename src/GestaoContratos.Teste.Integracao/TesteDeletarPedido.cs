﻿using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Teste.Integracao.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste.Integracao
{
    [TestClass]
    public class TesteDeletarPedido
    {
        private ITesteProcesso _testeProcesso;
        private IContratoProcesso _contratoProcesso;
        private IPedidoProcesso _pedidoProcesso;

        [TestInitialize]
        public void IniciarTestes()
        {
            _testeProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<ITesteProcesso>();
            _contratoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IContratoProcesso>();
            _pedidoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IPedidoProcesso>();

            _testeProcesso.IniciarTestes();            
            _contratoProcesso.InserirContrato(new ContratoDto()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = true
            });
        }

        [TestMethod]
        public void Teste_DeletarPedido_NaoAtendido()
        {
            _pedidoProcesso.InserirPedido(new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

            _pedidoProcesso.DeletarPedido(1, 1);

            var pedido = _pedidoProcesso.ObterPedido(1, 1);
            Assert.IsNull(pedido);
        }

        [TestMethod]        
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_EXCLUSAO)]
        public void Teste_DeletarPedido_Atendido()
        {
            _pedidoProcesso.InserirPedido(new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

            _pedidoProcesso.AtenderPedido(1, 1);
            _pedidoProcesso.DeletarPedido(1, 1);
        }
    }
}
