using GestaoContratos.Teste.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class TesteInicializacao
    {
        [AssemblyInitialize]
        public static void IniciarAplicacao(TestContext context)
        {
            UtilBancoDados.CriarBanco();
        }
    }
}
