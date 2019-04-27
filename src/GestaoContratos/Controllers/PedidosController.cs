using GestaoContratos.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class PedidosController : ApiController
    {
        [HttpGet]
        [Route("contratos/{contratoId}/pedidos")]
        [ResponseType(typeof(IList<PedidoDto>))]
        public IHttpActionResult ObterListaContrato(int contratoId)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}