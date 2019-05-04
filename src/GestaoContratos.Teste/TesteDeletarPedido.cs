using GestaoContratos.Dominio;
using GestaoContratos.Processo;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteDeletarPedido
    {
        [TestInitialize]
        public void IniciarTestes()
        {
            var testeProcesso = new TesteProcesso();
            testeProcesso.IniciarTestes();
            var contratoProcesso = new ContratoProcesso();
            contratoProcesso.InserirContrato(new Contrato()
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
            var pedidoProcesso = new PedidoProcesso();

            pedidoProcesso.InserirPedido(new Pedido()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

            pedidoProcesso.DeletarPedido(1, 1);

            var pedido = pedidoProcesso.ObterPedido(1, 1);
            Assert.IsNull(pedido);
        }
    }
}
