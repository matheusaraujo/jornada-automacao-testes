using GestaoContratos.Dominio;
using GestaoContratos.Processo;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteObterContratos
    {

        [TestInitialize]
        public void IniciarTestes()
        {
            var testeProcesso = new TesteProcesso();
            testeProcesso.IniciarTestes();
        }

        [TestMethod]
        public void Teste_ObterContratos_Vazio()
        {
            var contratoProcesso = new ContratoProcesso();
            var contratos = contratoProcesso.ObterContratos();
            Assert.IsTrue(contratos == null || contratos.Count == 0);
        }

        [TestMethod]
        public void Teste_ObterContratos_UmContrato()
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
            Assert.IsNotNull(contratos);
            Assert.AreEqual(1, contratos.Count);
        }
    }
}
