using System.Configuration;

namespace GestaoContratos.Util
{
    public static class Configuracao
    {
        public static string Diretorio
        {
            get
            {
                return System.AppContext.BaseDirectory;
            }
        }

        public static string CaminhoBancoDados
        {
            get
            {
                return ConfigurationManager.AppSettings["caminhoBancoDados"];
            }
        }

        public static string TipoTesteIntegracao
        {
            get
            {
                return ConfigurationManager.AppSettings["tipoTesteIntegracao"];
            }
        }
    }
}
