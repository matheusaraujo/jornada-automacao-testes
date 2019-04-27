using GestaoContratos.Models;
using GestaoContratos.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class ContratosController : ApiController
    {
        [HttpGet]       
        [Route("contratos")]
        [ResponseType(typeof(IList<ContratoDto>))]
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
        [ResponseType(typeof(int))]
        public IHttpActionResult InserirContrato([FromBody] ContratoDto contrato)
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
        [ResponseType(typeof(ContratoDto))]
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
        [ResponseType(typeof(void))]
        public IHttpActionResult AtualizarContrato(int contratoId, [FromBody] ContratoDto contrato)
        {
            try
            {
                Repositorio.AtualizarContrato(contratoId, contrato);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("contratos/{contratoId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult RemoverContrato(int contratoId)
        {
            try
            {
                Repositorio.DeletarContrato(contratoId);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
