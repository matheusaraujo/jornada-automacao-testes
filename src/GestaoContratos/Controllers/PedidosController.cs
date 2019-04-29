using GestaoContratos.Models;
using GestaoContratos.Repository;
using GestaoContratos.Utils;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class PedidosController : BaseApiController
    {
        [HttpGet]
        [Route("contratos/{contratoId}/pedidos")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedidos", typeof(IList<Pedido>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]        
        public IHttpActionResult ObterPedidos(int contratoId)
        {
            try
            {
                var pedidos = Repositorio.ObterPedidos(contratoId);
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound();
                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("contratos/{contratoId}/pedidos")]
        [SwaggerResponse(HttpStatusCode.Created, "Criado")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Conflito")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult InserirPedido(int contratoId, [FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    return Conflict();

                if (pedido.Volume < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
                else if (pedido.DataPedido < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);
                else if (pedido.Atendido)
                    throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoInsercao);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
                else if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
                else if (contrato.VolumeDisponivel < pedido.Volume)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumeContratoInsuficiente);
                else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
                else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

                int pedidoId = Repositorio.InserirPedido(pedido);

                contrato.VolumeDisponivel -= pedido.Volume;
                Repositorio.EditarVolumeContrato(contrato);

                return Created(Request.RequestUri + pedidoId.ToString(), pedidoId);
            }
            catch (RegraNegocioException e)
            {
                return Content(HttpStatusCode.PreconditionFailed, e.Serializar());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedido", typeof(Pedido))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult ObterPedido(int contratoId, int pedidoId)
        {
            try
            {
                var pedidos = Repositorio.ObterPedido(contratoId, pedidoId);
                if (pedidos == null)
                    return NotFound();
                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Conflito")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult EditarPedido(int contratoId, int pedidoId, [FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    return Conflict();
                else if (pedido.PedidoId != pedidoId)
                    return Conflict();

                if (pedido.Volume < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
                else if (pedido.DataPedido < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
                else if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
                else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
                else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

                var pedidoAtual = Repositorio.ObterPedido(contratoId, pedidoId);

                if (pedidoAtual == null)
                    return NotFound();
                else if (pedidoAtual.Atendido)
                    throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoEdicao);
                else if (pedido.Volume > contrato.VolumeDisponivel + pedidoAtual.Volume)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumeContratoInsuficiente);

                Repositorio.EditarPedido(pedido);
                contrato.VolumeDisponivel = contrato.VolumeDisponivel + pedidoAtual.Volume - pedido.Volume;
                Repositorio.EditarVolumeContrato(contrato);
                return NoContent();
            }
            catch (RegraNegocioException e)
            {
                return Content(HttpStatusCode.PreconditionFailed, e.Serializar());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Removido")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult DeletarPedido(int contratoId, int pedidoId)
        {
            try
            {
                var pedido = Repositorio.ObterPedido(contratoId, pedidoId);
                if (pedido == null)
                    return NotFound();
                else if (pedido.Atendido)
                    throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoExclusao);

                Repositorio.DeletarPedido(contratoId, pedidoId);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);

                contrato.VolumeDisponivel += pedido.Volume;
                Repositorio.EditarVolumeContrato(contrato);

                return NoContent();
            }
            catch (RegraNegocioException e)
            {
                return Content(HttpStatusCode.PreconditionFailed, e.Serializar());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPatch]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}/atendido")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult AtenderPedido(int contratoId, int pedidoId)
        {
            try
            {
                var pedido = Repositorio.ObterPedido(contratoId, pedidoId);
                if (pedido == null)
                    return NotFound();
                else if (pedido.Atendido)
                    throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalidoEdicao);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
                else if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);

                pedido.Atendido = true;
                Repositorio.EditarPedido(pedido);

                return NoContent();
            }
            catch (RegraNegocioException e)
            {
                return Content(HttpStatusCode.PreconditionFailed, e.Serializar());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}