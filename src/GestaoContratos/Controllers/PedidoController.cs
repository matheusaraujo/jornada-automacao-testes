using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class PedidoController : BaseApiController
    {
        private readonly IPedidoProcesso _pedidoProcesso;

        public PedidoController(IPedidoProcesso pedidoProcesso)
        {
            _pedidoProcesso = pedidoProcesso;
        }

        [HttpGet]
        [Route("contratos/{contratoId}/pedidos")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedidos", typeof(IList<PedidoDto>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]        
        public IHttpActionResult ObterPedidos(int contratoId)
        {
            try
            {
                var pedidos = _pedidoProcesso.ObterPedidos(contratoId);
                if (pedidos == null || pedidos.Count == 0)
                    return NotFound();
                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedido", typeof(PedidoDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult ObterPedido(int contratoId, int pedidoId)
        {
            try
            {
                var pedido = _pedidoProcesso.ObterPedido(contratoId, pedidoId);
                if (pedido == null)
                    return NotFound();
                return Ok(pedido);
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
        public IHttpActionResult InserirPedido(int contratoId, [FromBody] PedidoDto pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    return Conflict();

                var pedidoId = _pedidoProcesso.InserirPedido(pedido);

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

        [HttpPut]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Conflito")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult EditarPedido(int contratoId, int pedidoId, [FromBody] PedidoDto pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    return Conflict();
                else if (pedido.PedidoId != pedidoId)
                    return Conflict();

                var pedidoEditado = _pedidoProcesso.EditarPedido(pedido);
                if (pedidoEditado)
                    return NoContent();
                else
                    return NotFound();
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
                var pedidoDeletado = _pedidoProcesso.DeletarPedido(contratoId, pedidoId);
                if (pedidoDeletado)
                    return NoContent();
                else
                    return NotFound();
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
                var pedidoAtendido = _pedidoProcesso.AtenderPedido(contratoId, pedidoId);
                if (pedidoAtendido)
                    return NoContent();
                else
                    return NotFound();
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