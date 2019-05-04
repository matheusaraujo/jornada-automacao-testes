using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteObterContratos
    {
        private ITesteProcesso _testeProcesso;
        private IContratoProcesso _contratoProcesso;

        [TestInitialize]
        public void IniciarTestes()
        {   
            _testeProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<ITesteProcesso>();
            _contratoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IContratoProcesso>();
            
            _testeProcesso.IniciarTestes();
        }

        [TestMethod]
        public void Teste_ObterContratos_Vazio()
        {   
            var contratos = _contratoProcesso.ObterContratos();
            Assert.IsTrue(contratos == null || contratos.Count == 0);
        }

        [TestMethod]
        public void Teste_ObterContratos_UmContrato()
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
            Assert.IsNotNull(contratos);
            Assert.AreEqual(1, contratos.Count);
        }
    }
}
