using GestaoContratos.Teste.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class Inicializacao
    {
        [AssemblyInitialize]
        public static void IniciarAplicacao(TestContext context)
        {
            UtilBancoDados.CriarBanco();
            InjetorDependencias.InjetorDependencias.IniciarMock();
        }
    }
}
