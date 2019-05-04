using GestaoContratos.Interface.Repositorio;
using GestaoContratos.Repositorio.Base;

namespace GestaoContratos.Repositorio
{
    public class TesteRepositorio : RepositorioBase, ITesteRepositorio
    {
        public void IniciarTestes()
        {
            var sql = @"DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato';
                DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'";

            Executar(sql);
        }
    }
}
