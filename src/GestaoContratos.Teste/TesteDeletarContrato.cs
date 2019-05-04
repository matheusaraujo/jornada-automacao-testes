using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Teste.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteDeletarContrato
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
        }

        [TestMethod]
        public void Teste_DeletarContrato_SemPedidos_Sucesso()
        {
            _contratoProcesso.InserirContrato(new ContratoDto()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = true
            });

            var contratos = _contratoProcesso.ObterContratos();
            Assert.AreEqual(1, contratos.Count);

            _contratoProcesso.DeletarContrato(1);

            contratos = _contratoProcesso.ObterContratos();
            Assert.IsTrue(contratos == null || contratos.Count == 0);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(RegraNegocioException), ConstantesRegraNegocio.CONTRATO_POSSUI_PEDIDOS)]
        public void Teste_DeletarContrato_ComPedidos_Erro()
        {
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

            var contratos = _contratoProcesso.ObterContratos();
            Assert.AreEqual(1, contratos.Count);

            _contratoProcesso.DeletarContrato(1);

        }
    }
}
