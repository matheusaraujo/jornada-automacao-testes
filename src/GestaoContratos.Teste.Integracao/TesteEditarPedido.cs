using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Teste.Integracao.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste.Integracao
{
    [TestClass]
    public class TesteEditarPedido
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

            _pedidoProcesso.InserirPedido(new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

        }

        [TestMethod]
        [Description("O volume do pedido deve ser maior ou igual a 1.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.VOLUME_PEDIDO_INVALIDO)]
        public void Erro3001()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 0.5f,
                DataPedido = ExtensaoDateTime.DataAtual()
            };

            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("A data do pedido deve ser maior ou igual à data atual.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.DATA_PEDIDO_INVALIDA)]
        public void Erro3002()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual().AddDays(-1)
            };

            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("O contrato do pedido deve estar ativo.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.CONTRATO_INATIVO)]
        public void Erro3005()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual()
            };

            _contratoProcesso.EditarContrato(new ContratoDto()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = false
            });

            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("O volume do pedido deve ser menor ou igual ao volume disponível do contrato.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.VOLUME_CONTRATO_INSUFICIENTE)]
        public void Erro3006()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 999,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("A data do pedido deve estar entre as datas de vigência do contrato.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.DATA_PEDIDO_FORA_VIGENCIA_CONTRATO)]
        public void Erro3007()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataFimMesAtual().AddDays(1),
                Atendido = false
            };

            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("O pedido deve ter status não atendido para ser editado.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_EDICAO)]
        public void Erro3008()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 6,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            _pedidoProcesso.AtenderPedido(1, 1);
            _pedidoProcesso.EditarPedido(pedido);
        }

        [TestMethod]
        [Description("Pedido editado com sucesso.")]
        public void Sucesso()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 10,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            _pedidoProcesso.EditarPedido(pedido);

            var pedidoBanco = _pedidoProcesso.ObterPedido(1, 1);

            Assert.AreEqual(pedido.PedidoId, pedidoBanco.PedidoId);
            Assert.AreEqual(pedido.ContratoId, pedidoBanco.ContratoId);
            Assert.AreEqual(pedido.Volume, pedidoBanco.Volume);
            Assert.AreEqual(pedido.Atendido, pedidoBanco.Atendido);

            var contrato = _contratoProcesso.ObterContrato(1);
            Assert.AreEqual(90, contrato.VolumeDisponivel);
        }

    }
}
