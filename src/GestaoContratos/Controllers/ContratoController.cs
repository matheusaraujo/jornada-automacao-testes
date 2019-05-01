using GestaoContratos.Dominio;
using GestaoContratos.Processo;
using GestaoContratos.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class ContratoController : BaseApiController
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
                var contratos = new ContratoProcesso().ObterContratos();
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
                var contratoId = new ContratoProcesso().InserirContrato(contrato);
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
                var contrato = new ContratoProcesso().ObterContrato(contratoId);
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

                var contratoEditado = new ContratoProcesso().EditarContrato(contrato);

                if (contratoEditado)
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
        [Route("contratos/{contratoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Removido")]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, "Erro na requisição")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult DeletarContrato(int contratoId)
        {
            try
            {
                var contratoDeletado = new ContratoProcesso().DeletarContrato(contratoId);
                if (contratoDeletado)
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
