using GestaoContratos.Repository;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class TestesController : BaseApiController
    {
        [HttpPost]
        [Route("testes")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult IniciarTestes()
        {
            try
            {
                Repositorio.IniciarTestes();
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}