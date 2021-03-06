﻿using GestaoContratos.Interface.Processo;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Web.Http;

namespace GestaoContratos.Controllers
{
    [RoutePrefix("api/v1")]
    public class TesteController : BaseApiController
    {
        private readonly ITesteProcesso _testeProcesso;

        public TesteController(ITesteProcesso testeProcesso)
        {
            _testeProcesso = testeProcesso;
        }

        [HttpPost]
        [Route("testes")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Alterado")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Erro interno")]
        public IHttpActionResult IniciarTestes()
        {
            try
            {
                _testeProcesso.IniciarTestes();
                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}