using System;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio
{
    public class TesteRepositorio
    {
        public void IniciarTestes()
        {
            var sql = @"DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato';
                DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'";

            using (var conexao = new SQLiteConnection($"DataSource={AppContext.BaseDirectory}\\App_Data\\database.sqlite;Version=3"))
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
