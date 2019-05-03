using System;
using System.Configuration;
using System.Data.SQLite;

namespace GestaoContratos.Repositorio.Base
{
    public abstract class RepositorioBase
    {
        private static string Diretorio
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }

        private static string CaminhoBancoDados
        {
            get
            {
                return ConfigurationManager.AppSettings["caminhoBancoDados"];
            }
        }

        public static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Diretorio, CaminhoBancoDados));
        }
    }
}
