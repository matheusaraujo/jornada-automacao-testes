using System;

namespace GestaoContratos.Dominio.Entidade
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ContratoId { get; set; }
        public float Volume { get; set; }
        public DateTime DataPedido { get; set; }
        public bool Atendido { get; set; }
    }
}
