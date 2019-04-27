using GestaoContratos.Models;
using GestaoContratos.Repository;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class PedidosController : ApiController
    {
        [HttpGet]
        [Route("contratos/{contratoId}/pedidos")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedidos", typeof(IList<Contrato>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]        
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
        [SwaggerResponse(HttpStatusCode.Created, "Pedido", typeof(int))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult InserirPedido(int contratoId, [FromBody] Pedido pedido)
        {
            try
            {
                int pedidoId = Repositorio.InserirPedido(contratoId, pedido);
                return Created(Request.RequestUri + pedidoId.ToString(), pedidoId);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [ResponseType(typeof(IList<Pedido>))]
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
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult AtualizarPedido(int contratoId, int pedidoId, [FromBody] Pedido pedido)
        {
            try
            {
                Repositorio.AtualizarPedido(contratoId, pedidoId, pedido);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("contratos/{contratoId}/pedidos/{pedidoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Removido")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult DeletarPedido(int contratoId, int pedidoId)
        {
            try
            {
                Repositorio.DeletarPedido(contratoId, pedidoId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}