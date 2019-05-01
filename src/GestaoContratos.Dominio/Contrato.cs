using System;

namespace GestaoContratos.Dominio
{
    public class Contrato
    {
        public int ContratoId { get; set; }
        public float VolumeDisponivel { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public bool Ativo { get; set; }
    }
}
