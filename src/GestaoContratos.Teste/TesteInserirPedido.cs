using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Processo;
using GestaoContratos.Teste.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteInserirPedido
    {
        private ITesteProcesso _testeProcesso;
        private IContratoProcesso _contratoProcesso;
        private IPedidoProcesso _pedidoProcesso;

        [TestInitialize]
        public void IniciarTestes()
        {

            InjetorDependencias.InjetorDependencias.Iniciar();
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

            _contratoProcesso.InserirContrato(new ContratoDto()
            {
                ContratoId = 2,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = false
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

            _pedidoProcesso.InserirPedido(pedido);
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

            _pedidoProcesso.InserirPedido(pedido);
        }

        [TestMethod]
        [Description("O pedido deve ser criado com status não atendido.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_INSERCAO)]
        public void Erro3003()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = true
            };

            _pedidoProcesso.InserirPedido(pedido);
        }

        [TestMethod]
        [Description("O pedido deve ser criado para um contrato existente.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.CONTRATO_INEXISTENTE)]
        public void Erro3004()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 999,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            _pedidoProcesso.InserirPedido(pedido);
        }

        [TestMethod]
        [Description("O contrato do pedido deve estar ativo.")]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.CONTRATO_INATIVO)]
        public void Erro3005()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 2,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            _pedidoProcesso.InserirPedido(pedido);
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

            _pedidoProcesso.InserirPedido(pedido);
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

            _pedidoProcesso.InserirPedido(pedido);
        }

        [TestMethod]
        [Description("Pedido criado com sucesso.")]
        public void Sucesso()
        {
            var pedido = new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            };

            int pedidoId = _pedidoProcesso.InserirPedido(pedido);

            var pedidoBanco = _pedidoProcesso.ObterPedido(1, pedidoId);

            Assert.AreEqual(pedido.PedidoId, pedidoBanco.PedidoId);
            Assert.AreEqual(pedido.ContratoId, pedidoBanco.ContratoId);
            Assert.AreEqual(pedido.Volume, pedidoBanco.Volume);
            Assert.AreEqual(pedido.Atendido, pedidoBanco.Atendido);

            var contrato = _contratoProcesso.ObterContrato(1);
            Assert.AreEqual(95, contrato.VolumeDisponivel);

        }
    }
}

