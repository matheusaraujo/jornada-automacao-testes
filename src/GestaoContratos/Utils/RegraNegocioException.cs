using System;

namespace GestaoContratos.Utils
{
    public class RegraNegocioException : Exception
    {
        public RegraNegocioEnum CodigoErro { get; }

        public RegraNegocioException(RegraNegocioEnum e) : base(ObterMensagem(e))
        {
            CodigoErro = e;
        }

        private static string ObterMensagem(RegraNegocioEnum e)
        {
            if (e == RegraNegocioEnum.ContratoInvalido)
                return "O contrato informado é inválido!";
            else if (e == RegraNegocioEnum.PedidoInvalido)
                return "O pedido informado é inválido!";
            else if (e == RegraNegocioEnum.DataInicioVigenciaInvalida)
                return "A data de início de vigência do contrato deve ser menor ou igual à data atual!";
            else if (e == RegraNegocioEnum.DataFimVigenciaInvalida)
                return "A data de fim de vigência do contrato deve ser maior ou igual à data atual!";
            else if (e == RegraNegocioEnum.VolumeDisponivelInvalido)
                return "O volume disponível do contrato deve ser maior ou igual a 1!";
            else if (e == RegraNegocioEnum.ContratoPossuiPedidos)
                return "O contrato não pode possuir pedidos para ser removido!";
            else if (e == RegraNegocioEnum.VolumePedidoInvalido)
                return "O volume do pedido deve ser maior ou igual a 1!";
            else if (e == RegraNegocioEnum.DataPedidoInvalida)
                return "A data do pedido deve ser maior ou igual à data atual!";
            else if (e == RegraNegocioEnum.ContratoInexistente)
                return "O pedido deve ser criado para um contrato existente!";
            else if (e == RegraNegocioEnum.ContratoInativo)
                return "O contrato do pedido deve estar ativo!";
            else if (e == RegraNegocioEnum.VolumePedidoMaiorVolumeDisponivel)
                return "O volume do pedido deve ser menor ou igual ao volume disponível do contrato!";
            else if (e == RegraNegocioEnum.DataPedidoForaVigenciaContrato)
                return "A data do pedido deve estar entre as datas de vigência do contrato!";
            else if (e == RegraNegocioEnum.StatusPedidoInvalido)
                return "O pedido deve ter status não atendido para ser excluído!";
            else
                return "Erro não esperado!";
        }

        public object Serializar()
        {
            return new
            {
                CodigoErro,
                Mensagem = Message
            };
        }
    }

    public enum RegraNegocioEnum
    {
        ContratoInvalido = 1001,
        PedidoInvalido = 1001,

        DataInicioVigenciaInvalida = 2001,
        DataFimVigenciaInvalida = 2002,
        VolumeDisponivelInvalido = 2003,
        ContratoPossuiPedidos = 2004,

        VolumePedidoInvalido = 3001,
        DataPedidoInvalida = 3002,
        ContratoInexistente = 3003,
        ContratoInativo = 3004,
        VolumePedidoMaiorVolumeDisponivel = 3005,
        DataPedidoForaVigenciaContrato = 3006,
        StatusPedidoInvalido = 3007
    }

}