using GestaoContratos.Interface.Processo;
using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Processo;
using GestaoContratos.Repositorio;
using SimpleInjector;

namespace GestaoContratos.InjetorDependencias
{
    public static class InjetorDependencias
    {
        public static Container Container { get; private set; }

        public static void Iniciar()
        {
            if (Container != null)
                Container.Dispose();

            Container = new Container();

            Container.Register<IContratoRepositorio, ContratoRepositorio>();
            Container.Register<IPedidoRepositorio, PedidoRepositorio>();
            Container.Register<ITesteRepositorio, TesteRepositorio>();

            Container.Register<IContratoProcesso, ContratoProcesso>();
            Container.Register<IPedidoProcesso, PedidoProcesso>();
            Container.Register<ITesteProcesso, TesteProcesso>();

            Container.Verify();
        }

        public static T ObterInstancia<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
