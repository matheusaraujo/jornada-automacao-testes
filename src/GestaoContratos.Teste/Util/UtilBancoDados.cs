using GestaoContratos.Util;
using System.Data.SQLite;

namespace GestaoContratos.Teste.Util
{
    public static class UtilBancoDados
    {   
        private static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Configuracao.Diretorio, Configuracao.CaminhoBancoDados));
        }

        public static void Executar(this SQLiteCommand comando)
        {
            if (comando.Connection.State == System.Data.ConnectionState.Closed)
                comando.Connection.Open();
            comando.ExecuteNonQuery();
        }

        private static void Executar(string sql)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                    comando.Executar();
            }
        }

        public static void CriarBanco()
        {
            SQLiteConnection.CreateFile("tmp_database.sqlite");

            var sqlCreateTableContrato = @"CREATE TABLE ""Contrato"" ( `ContratoId` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `VolumeDisponivel` NUMERIC NOT NULL, `DataInicioVigencia` INTEGER NOT NULL, `DataFimVigencia` INTEGER NOT NULL, `Ativo` INTEGER NOT NULL )";
            var sqlCreateTablePedido = @"CREATE TABLE ""Pedido"" ( `PedidoId` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `ContratoId` INTEGER NOT NULL, `Volume` NUMERIC NOT NULL, `DataPedido` INTEGER NOT NULL, `Atendido` INTEGER NOT NULL )";

            Executar(sqlCreateTableContrato);
            Executar(sqlCreateTablePedido);
        }
    }
}
