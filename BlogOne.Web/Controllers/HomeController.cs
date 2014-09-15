using System.Configuration;
using System.Net;
using System.Web.Configuration;
using BlogOne.Common.Extensions;
using BlogOne.Web.Data;
using System.Linq;
using System.Web.Mvc;
using BlogOne.Web.Views.Home;

namespace BlogOne.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;

        public HomeController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

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

            var posts = _postRepository.FindPublished(1, 5)
                .Select(p => PostController.ViewModelFromPost(p, PostController.PostViewModelOptions.Excerpt));
            return View(posts);
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
        public ActionResult SetupSubmit(SetupViewModel viewModel)
        {
            var initialized = bool.Parse(WebConfigurationManager.AppSettings["BlogOne:Initialized"]);
            if (initialized)
                throw new ConfigurationErrorsException("Configuration has already been initialized; To change configured settings, please use the settings page or edit the configuration manually.");

            var config = WebConfigurationManager.OpenWebConfiguration("~");

            if (config.AppSettings.Settings.AllKeys.Contains(AppSettingsKeys.Title))
            {
                config.AppSettings.Settings[AppSettingsKeys.Title].Value = viewModel.BlogTitle;
            }
            else
            {
                config.AppSettings.Settings.Add(AppSettingsKeys.Title, viewModel.BlogTitle);
            }

            if (viewModel.Name.HasValue())
            {
                if (config.AppSettings.Settings.AllKeys.Contains(AppSettingsKeys.Name))
                {
                    config.AppSettings.Settings[AppSettingsKeys.Name].Value = viewModel.Name;
                }
                else
                {
                    config.AppSettings.Settings.Add(AppSettingsKeys.Name, viewModel.Name);
                }
            }

            config.AppSettings.Settings[AppSettingsKeys.Initialized].Value = "True";

            config.Save();
            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}