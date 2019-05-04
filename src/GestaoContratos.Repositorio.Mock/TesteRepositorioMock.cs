using GestaoContratos.Dominio.Entidade;
using GestaoContratos.Interface.Repositorio;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    public class TesteRepositorioMock : ITesteRepositorio
    {
        public void IniciarTestes()
        {
            BancoMemoria.Iniciar();
        }
    }
}
