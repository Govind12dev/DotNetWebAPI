﻿using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;

namespace WebAPI
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
