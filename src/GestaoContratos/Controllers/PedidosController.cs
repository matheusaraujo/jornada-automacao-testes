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
    public class PedidosController : ApiController
    {
        [HttpGet]
        [Route("contratos/{contratoId}/pedidos")]
        [SwaggerResponse(HttpStatusCode.OK, "Pedidos", typeof(IList<Contrato>))]
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
        [SwaggerResponse(HttpStatusCode.Created, "Criado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult InserirPedido(int contratoId, [FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInvalido);
                else if (pedido.Volume < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
                else if (pedido.DataPedido < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
                else if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
                else if (contrato.VolumeDisponivel < pedido.Volume)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoMaiorVolumeDisponivel);
                else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
                else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

                int pedidoId = Repositorio.InserirPedido(pedido);
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
        [SwaggerResponse(HttpStatusCode.OK, "Pedido", typeof(IList<Contrato>))]
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
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult AtualizarPedido(int contratoId, int pedidoId, [FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.ContratoId != contratoId)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInvalido);
                else if (pedido.PedidoId != pedidoId)
                    throw new RegraNegocioException(RegraNegocioEnum.PedidoInvalido);
                else if (pedido.Volume < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoInvalido);
                else if (pedido.DataPedido < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoInvalida);

                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInexistente);
                else if (!contrato.Ativo)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoInativo);
                else if (contrato.VolumeDisponivel < pedido.Volume)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumePedidoMaiorVolumeDisponivel);
                else if (contrato.DataInicioVigencia.Date > pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);
                else if (contrato.DataFimVigencia.Date < pedido.DataPedido.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataPedidoForaVigenciaContrato);

                var pedidoAtual = Repositorio.ObterPedido(contratoId, pedidoId);

                if (pedidoAtual == null)
                    return NotFound();

                Repositorio.AtualizarPedido(pedido);
                return StatusCode(HttpStatusCode.NoContent);
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
                    throw new RegraNegocioException(RegraNegocioEnum.StatusPedidoInvalido);

                Repositorio.DeletarPedido(contratoId, pedidoId);
                return StatusCode(HttpStatusCode.NoContent);
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