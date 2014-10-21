using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

using BlogOne.Common.Extensions;
using BlogOne.Web.Data;
using BlogOne.Web.Extensions;
using BlogOne.Web.Views.Home;

namespace BlogOne.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            var initialized = false;
            if (!bool.TryParse(WebConfigurationManager.AppSettings["BlogOne:Initialized"], out initialized))
                throw new ConfigurationErrorsException();

            if (!initialized)
            {
                return RedirectToAction("Setup");
            }

            //var posts = _postRepository.FindPublished(1, 5)
            //    .Select(p => PostController.ViewModelFromPost(p, PostController.PostViewModelOptions.Excerpt));
            //return View(posts);
            return Content("index");
        }

        [Route("setup")]
        public ActionResult Setup()
        {
            var initialized = bool.Parse(WebConfigurationManager.AppSettings["BlogOne:Initialized"]);
            if (initialized)
                throw new ConfigurationErrorsException("Configuration has already been initialized; To change configured settings, please use the settings page or edit the configuration manually.");
            
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("setup/submit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> SetupSubmitAsync(SetupViewModel viewModel, CancellationToken cancellationToken)
        {
            var initialized = bool.Parse(WebConfigurationManager.AppSettings["BlogOne:Initialized"]);
            if (initialized)
                throw new ConfigurationErrorsException("Configuration has already been initialized; To change configured settings, please use the settings page or edit the configuration manually.");

            var config = WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings.Ensure(AppSettingsKeys.Title, viewModel.BlogTitle);

            if (viewModel.Name.HasValue())
            {
                config.AppSettings.Settings.Ensure(AppSettingsKeys.Name, viewModel.Name);
            }

            await Task.Factory.StartNew(config.Save, cancellationToken);

            return RedirectToAction("Index", "Admin");
        }
    }
}