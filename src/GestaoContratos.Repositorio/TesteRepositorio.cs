using GestaoContratos.Repositorio.Base;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio
{
    public class TesteRepositorio : RepositorioBase
    {
        public void IniciarTestes()
        {
            var sql = @"DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato';
                DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'";

            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Connection.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
