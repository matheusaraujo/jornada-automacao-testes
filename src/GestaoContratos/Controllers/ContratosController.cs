using GestaoContratos.Models;
using GestaoContratos.Repository;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class ContratosController : ApiController
    {
        [HttpGet]       
        [Route("contratos")]        
        [SwaggerResponse(HttpStatusCode.OK, "Contratos", typeof(IList<Contrato>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Não encontrado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
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
        [SwaggerResponse(HttpStatusCode.Created, "Contrato", typeof(int))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult InserirContrato([FromBody] Contrato contrato)
        {
            try
            {
                int contratoId = Repositorio.InserirContrato(contrato);
                return Created(Request.RequestUri + contratoId.ToString(), contratoId);
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
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
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
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult AtualizarContrato(int contratoId, [FromBody] Contrato contrato)
        {
            try
            {
                Repositorio.AtualizarContrato(contratoId, contrato);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("contratos/{contratoId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Removido")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro")]
        public IHttpActionResult DeletarContrato(int contratoId)
        {
            try
            {
                Repositorio.DeletarContrato(contratoId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
