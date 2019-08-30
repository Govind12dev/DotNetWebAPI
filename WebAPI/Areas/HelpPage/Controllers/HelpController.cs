using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.Areas.HelpPage.ModelDescriptions;
using WebAPI.Areas.HelpPage.Models;


namespace WebAPI.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>

    [ExcludeFromCodeCoverage]
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        [ExcludeFromCodeCoverage]
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        [ExcludeFromCodeCoverage]
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        [ExcludeFromCodeCoverage]
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        [ExcludeFromCodeCoverage]
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        [ExcludeFromCodeCoverage]
        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}