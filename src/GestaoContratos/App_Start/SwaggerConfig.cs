using GestaoContratos;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GestaoContratos
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "GestaoContratos")
                        .Description("API Gestão de Contratos");
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Gestão de Contratos");
                });
        }
    }
}
