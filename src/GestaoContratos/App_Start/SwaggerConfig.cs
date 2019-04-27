using System.Web.Http;
using WebActivatorEx;
using GestaoContratos;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GestaoContratos
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

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
