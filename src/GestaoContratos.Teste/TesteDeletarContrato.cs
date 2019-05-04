using GestaoContratos.Dominio;
using GestaoContratos.Processo;
using GestaoContratos.Teste.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteDeletarContrato
    {

        [TestInitialize]
        public void IniciarTestes()
        {
            var testeProcesso = new TesteProcesso();
            testeProcesso.IniciarTestes();
        }

        [TestMethod]
        public void Teste_DeletarContrato_SemPedidos_Sucesso()
        {
            var contratoProcesso = new ContratoProcesso();

            contratoProcesso.InserirContrato(new Contrato()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = true
            });

            var contratos = contratoProcesso.ObterContratos();
            Assert.AreEqual(1, contratos.Count);

            contratoProcesso.DeletarContrato(1);

            contratos = contratoProcesso.ObterContratos();
            Assert.IsTrue(contratos == null || contratos.Count == 0);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.CONTRATO_POSSUI_PEDIDOS)]
        public void Teste_DeletarContrato_ComPedidos_Erro()
        {
            var contratoProcesso = new ContratoProcesso();
            var pedidoProcesso = new PedidoProcesso();

            contratoProcesso.InserirContrato(new Contrato()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = true
            });

            pedidoProcesso.InserirPedido(new Pedido()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

            var contratos = contratoProcesso.ObterContratos();
            Assert.AreEqual(1, contratos.Count);

            contratoProcesso.DeletarContrato(1);

        }
    }
}
