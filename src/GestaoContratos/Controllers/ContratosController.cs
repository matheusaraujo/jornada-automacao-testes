using GestaoContratos.Models;
using GestaoContratos.Repository;
using GestaoContratos.Utils;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class ContratosController : BaseApiController
    {
        [HttpGet]       
        [Route("contratos")]        
        [SwaggerResponse(HttpStatusCode.OK, "Contratos", typeof(IList<Contrato>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult ObterContratos()
        {
            try
            {
                var contratos = Repositorio.ObterContratos();
                if (contratos == null || contratos.Count == 0)
                    return NotFound();
                return Ok(contratos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("contratos")]
        [SwaggerResponse(HttpStatusCode.Created, "Criado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult InserirContrato([FromBody] Contrato contrato)
        {
            try
            {
                if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataInicioVigenciaInvalida);
                else if (contrato.DataFimVigencia < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataFimVigenciaInvalida);
                else if (contrato.VolumeDisponivel < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumeDisponivelInvalido);

                int contratoId = Repositorio.InserirContrato(contrato);
                return Created(Request.RequestUri + contratoId.ToString(), contratoId);
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
        [Route("contratos/{contratoId}")]        
        [SwaggerResponse(HttpStatusCode.OK, "Contrato", typeof(Contrato))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult ObterContrato(int contratoId)
        {
            try
            {
                var contrato = Repositorio.ObterContrato(contratoId);
                if (contrato == null)
                    return NotFound();
                return Ok(contrato);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("contratos/{contratoId}")]        
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Conflito")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult EditarContrato(int contratoId, [FromBody] Contrato contrato)
        {
            try
            {
                if (contrato.ContratoId != contratoId)
                    return Conflict();
                    
                if (contrato.DataInicioVigencia.Date > DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataInicioVigenciaInvalida);
                else if (contrato.DataFimVigencia < DateTime.Now.Date)
                    throw new RegraNegocioException(RegraNegocioEnum.DataFimVigenciaInvalida);
                else if (contrato.VolumeDisponivel < 1)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumeDisponivelInvalido);

                var contratoAtual = Repositorio.ObterContrato(contratoId);
                if (contratoAtual == null)
                    return NotFound();

                var pedidos = Repositorio.ObterPedidos(contratoId);
                var volumePedidosPendentes = pedidos.Where(p => !p.Atendido).Sum(p => p.Volume);
                if (contrato.VolumeDisponivel < volumePedidosPendentes)
                    throw new RegraNegocioException(RegraNegocioEnum.VolumeDisponivelInvalidoEdicao);

                Repositorio.EditarContrato(contrato);
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
        [Route("contratos/{contratoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Removido")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult DeletarContrato(int contratoId)
        {
            try
            {
                var contratoAtual = Repositorio.ObterContrato(contratoId);
                if (contratoAtual == null)
                    return NotFound();

                var pedidos = Repositorio.ObterPedidos(contratoId);
                if (pedidos != null && pedidos.Count > 0)
                    throw new RegraNegocioException(RegraNegocioEnum.ContratoPossuiPedidos);

                Repositorio.DeletarContrato(contratoId);
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
