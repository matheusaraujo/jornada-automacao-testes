using GestaoContratos.Teste.Util;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GestaoContratos.Teste
{
    [TestClass]
    public class Inicializacao
    {
        [AssemblyInitialize]
        public static void IniciarAplicacao(TestContext context)
        {

            if (Configuracao.TipoTesteIntegracao == "banco")
            {
                UtilBancoDados.CriarBanco();
                InjetorDependencias.InjetorDependencias.Iniciar();
            }
            else if (Configuracao.TipoTesteIntegracao == "mock")
            {
                InjetorDependencias.InjetorDependencias.IniciarMock();
            }
            else
            {
                throw new Exception("tipoTesteIntegracao invalido");
            }
        }
    }
}
