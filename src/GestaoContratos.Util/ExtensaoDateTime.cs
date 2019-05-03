using System;

namespace GestaoContratos.Util
{
    public static class ExtensaoDateTime
    {
        public static DateTime DataAtual()
        {
            return DateTime.Now.Date;
        }

        public static DateTime DataInicioMesAtual()
        {
            var hoje = DataAtual();
            return new DateTime(hoje.Year, hoje.Month, 1, 0, 0, 0);
        }

        public static DateTime DataFimMesAtual()
        {
            var hoje = DataAtual();
            return new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month), 0, 0, 0);
        }
    }
}
