using GestaoContratos.Util;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio.Base
{
    public abstract class RepositorioBase
    {
        public static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Configuracao.Diretorio, Configuracao.CaminhoBancoDados));
        }
    }
}
