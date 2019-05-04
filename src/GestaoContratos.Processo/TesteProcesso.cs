using GestaoContratos.Interface.Processo;
using GestaoContratos.Interface.Repositorio;

namespace GestaoContratos.Processo
{
    public class TesteProcesso : ITesteProcesso
    {
        private readonly ITesteRepositorio _testeRepositorio;

        public TesteProcesso(ITesteRepositorio testeRepositorio)
        {
            _testeRepositorio = testeRepositorio;
        }

        public void IniciarTestes()
        {
            _testeRepositorio.IniciarTestes();
        }
    }
}
