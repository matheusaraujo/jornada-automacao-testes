using System;
using System.ComponentModel;

namespace GestaoContratos.Util
{
    public class RegraNegocioException : Exception
    {
        public RegraNegocioEnum CodigoErro { get; }

        public RegraNegocioException(RegraNegocioEnum e) : base(e.GetDescription())
        {
            CodigoErro = e;
        }

        public object Serializar()
        {
            return new
            {
                CodigoErro,
                Mensagem = CodigoErro.GetDescription()
            };
        }
    }

    public enum RegraNegocioEnum
    {
        [Description("data_inicio_vigencia_invalida")]
        DataInicioVigenciaInvalida = 2001,

        [Description("data_fim_vigencia_invalida")]
        DataFimVigenciaInvalida = 2002,

        [Description("volume_disponivel_invalido")]
        VolumeDisponivelInvalido = 2003,

        [Description("volume_disponivel_invalido_edicao")]
        VolumeDisponivelInvalidoEdicao = 2004,

        [Description("contrato_possui_pedidos")]
        ContratoPossuiPedidos = 2005,

        [Description("volume_pedido_invalido")]
        VolumePedidoInvalido = 3001,

        [Description("data_pedido_invalida")]
        DataPedidoInvalida = 3002,

        [Description("status_pedido_invalido_insercao")]
        StatusPedidoInvalidoInsercao = 3003,

        [Description("contrato_inexistente")]
        ContratoInexistente = 3004,

        [Description("contrato_inativo")]
        ContratoInativo = 3005,

        [Description("volume_contrato_insuficiente")]
        VolumeContratoInsuficiente = 3006,

        [Description("data_pedido_fora_vigencia_contrato")]
        DataPedidoForaVigenciaContrato = 3007,

        [Description("status_pedido_invalido_edicao")]
        StatusPedidoInvalidoEdicao = 3008,

        [Description("status_pedido_invalido_exclusao")]
        StatusPedidoInvalidoExclusao = 3009
    }
}
