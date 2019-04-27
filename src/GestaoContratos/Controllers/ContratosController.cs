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
        public IHttpActionResult ObterListaContrato()
        {
            try
            {
                return Ok(Repositorio.ObterContratos());
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
    }
}
