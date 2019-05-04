using Newtonsoft.Json.Serialization;
using SimpleInjector.Integration.WebApi;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GestaoContratos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            var jsonConfig = config.Formatters.JsonFormatter;
            jsonConfig.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonConfig.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index")
            );

            InjetorDependencias.InjetorDependencias.Iniciar();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(InjetorDependencias.InjetorDependencias.Container);
        }
    }
}
