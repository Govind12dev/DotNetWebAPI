﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI
{
    public static class WebApiConfig
    {
        [ExcludeFromCodeCoverage]
        public static void Register(HttpConfiguration config)
        {
            //Enable CORS 
            config.EnableCors(new EnableCorsAttribute("http://localhost:80", headers: "*", methods: "*"));
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
