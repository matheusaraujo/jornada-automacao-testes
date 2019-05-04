using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GestaoContratos.Util
{
    [Serializable]
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException()
        {

        }

        public TipoRegraNegocio CodigoErro { get; }

        public RegraNegocioException(TipoRegraNegocio e) : base(e.GetDescription())
        {
            CodigoErro = e;
        }

        protected RegraNegocioException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
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

    public static class ConstantesRegraNegocio
    {
        public const string DATA_INICIO_VIGENCIA_INVALIDA = "data_inicio_vigencia_invalida";
        public const string DATA_FIM_VIGENCIA_INVALIDA = "data_fim_vigencia_invalida";
        public const string VOLUME_DISPONIVEL_INVALIDO = "volume_disponivel_invalido";
        public const string VOLUME_DISPONIVEL_INVALIDO_EDICAO = "volume_disponivel_invalido_edicao";
        public const string CONTRATO_POSSUI_PEDIDOS = "contrato_possui_pedidos";
        public const string VOLUME_PEDIDO_INVALIDO = "volume_pedido_invalido";
        public const string DATA_PEDIDO_INVALIDA = "data_pedido_invalida";
        public const string STATUS_PEDIDO_INVALIDO_INSERCAO = "status_pedido_invalido_insercao";
        public const string CONTRATO_INEXISTENTE = "contrato_inexistente";
        public const string CONTRATO_INATIVO = "contrato_inativo";
        public const string VOLUME_CONTRATO_INSUFICIENTE = "volume_contrato_insuficiente";
        public const string DATA_PEDIDO_FORA_VIGENCIA_CONTRATO = "data_pedido_fora_vigencia_contrato";
        public const string STATUS_PEDIDO_INVALIDO_EDICAO = "status_pedido_invalido_edicao";
        public const string STATUS_PEDIDO_INVALIDO_EXCLUSAO = "status_pedido_invalido_exclusao";
    }

    public enum TipoRegraNegocio
    {
        [Description(ConstantesRegraNegocio.DATA_INICIO_VIGENCIA_INVALIDA)]
        DataInicioVigenciaInvalida = 2001,

        [Description(ConstantesRegraNegocio.DATA_FIM_VIGENCIA_INVALIDA)]
        DataFimVigenciaInvalida = 2002,

        [Description(ConstantesRegraNegocio.VOLUME_DISPONIVEL_INVALIDO)]
        VolumeDisponivelInvalido = 2003,

        [Description(ConstantesRegraNegocio.VOLUME_DISPONIVEL_INVALIDO_EDICAO)]
        VolumeDisponivelInvalidoEdicao = 2004,

        [Description(ConstantesRegraNegocio.CONTRATO_POSSUI_PEDIDOS)]
        ContratoPossuiPedidos = 2005,

        [Description(ConstantesRegraNegocio.VOLUME_PEDIDO_INVALIDO)]
        VolumePedidoInvalido = 3001,

        [Description(ConstantesRegraNegocio.DATA_PEDIDO_INVALIDA)]
        DataPedidoInvalida = 3002,

        [Description(ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_INSERCAO)]
        StatusPedidoInvalidoInsercao = 3003,

        [Description(ConstantesRegraNegocio.CONTRATO_INEXISTENTE)]
        ContratoInexistente = 3004,

        [Description(ConstantesRegraNegocio.CONTRATO_INATIVO)]
        ContratoInativo = 3005,

        [Description(ConstantesRegraNegocio.VOLUME_CONTRATO_INSUFICIENTE)]
        VolumeContratoInsuficiente = 3006,

        [Description(ConstantesRegraNegocio.DATA_PEDIDO_FORA_VIGENCIA_CONTRATO)]
        DataPedidoForaVigenciaContrato = 3007,

        [Description(ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_EDICAO)]
        StatusPedidoInvalidoEdicao = 3008,

        [Description(ConstantesRegraNegocio.STATUS_PEDIDO_INVALIDO_EXCLUSAO)]
        StatusPedidoInvalidoExclusao = 3009
    }
}
