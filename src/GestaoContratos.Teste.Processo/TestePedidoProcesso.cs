using GestaoContratos.Dominio.Dto;
using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Processo;
using GestaoContratos.Teste.Processo.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GestaoContratos.Teste.Processo
{
    [TestClass]
    public class TestePedidoProcesso
    {
        [TestMethod]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.DATA_PEDIDO_FORA_VIGENCIA_CONTRATO)]
        public void TesteValidarPedido_ForaVigenciaContrato()
        {
            var mockContratoRepositorio = new Mock<IContratoRepositorio>();
            var mockPedidoRepositorio = new Mock<IPedidoRepositorio>();

            var pedido = new Pedido()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                Atendido = false,
                DataPedido = new DateTime(2019, 1, 25)
            };

            var contrato = new Contrato()
            {
                ContratoId = 1,
                Ativo = true,
                DataInicioVigencia = new DateTime(2019, 2, 1),
                DataFimVigencia = new DateTime(2019, 2, 28)
            };

            var pedidoProcesso = new PedidoProcesso(mockContratoRepositorio.Object, mockPedidoRepositorio.Object);

            pedidoProcesso.ValidarPedido(pedido, contrato, new DateTime(2019, 1, 15));
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_EDICAO)]
        public void TesteEditarPedido_StatusInvalido()
        {
            var mockContratoRepositorio = new Mock<IContratoRepositorio>();
            var mockPedidoRepositorio = new Mock<IPedidoRepositorio>();

            mockPedidoRepositorio.Setup(m => m.ObterPedido(It.Is<int>(id => id == 1), It.Is<int>(id => id == 1))).Returns(new Pedido()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                Atendido = true,
                DataPedido = ExtensaoDateTime.DataAtual()
            });

            mockContratoRepositorio.Setup(m => m.ObterContrato(It.Is<int>(id => id == 1))).Returns(new Contrato()
            {
                ContratoId = 1,
                Ativo = true,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                VolumeDisponivel = 100
            });

            var pedidoProcesso = new PedidoProcesso(mockContratoRepositorio.Object, mockPedidoRepositorio.Object);

            pedidoProcesso.EditarPedido(new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,                
                DataPedido = ExtensaoDateTime.DataAtual()
            });
        }
    }
}
