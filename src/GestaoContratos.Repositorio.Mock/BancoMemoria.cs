using GestaoContratos.Dominio.Entidade;
using System.Collections.Generic;

namespace GestaoContratos.Repositorio.Mock
{
    internal static class BancoMemoria
    {
        static BancoMemoria()
        {
            Contratos = new List<Contrato>();
            Pedidos = new List<Pedido>();
        }

        internal static void Iniciar()
        {
            Contratos.Clear();
            Pedidos.Clear();
        }

        internal static List<Contrato> Contratos { get; }

        internal static List<Pedido> Pedidos { get; }
    }
}
