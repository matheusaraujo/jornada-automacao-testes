﻿using Newtonsoft.Json.Serialization;
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

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index")
            );
        }
    }
}