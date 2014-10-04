using System.Configuration;
using System.Net;
using System.Threading;
using System.Web.Configuration;
using BlogOne.Common.Extensions;
using BlogOne.Web.Data;
using System.Linq;
using System.Web.Mvc;
using BlogOne.Web.Extensions;
using BlogOne.Web.Integration;
using BlogOne.Web.Views.Home;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using System.Threading.Tasks;

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
        public async Task<ActionResult> SetupSubmitAsync(SetupViewModel viewModel, CancellationToken cancellationToken)
        {
            var initialized = bool.Parse(WebConfigurationManager.AppSettings["BlogOne:Initialized"]);
            if (initialized)
                throw new ConfigurationErrorsException("Configuration has already been initialized; To change configured settings, please use the settings page or edit the configuration manually.");

            var config = WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings.Ensure(AppSettingsKeys.Title, viewModel.BlogTitle);            
            config.AppSettings.Settings.Ensure(AppSettingsKeys.GoogleClientId, viewModel.GoogleClientId);
            config.AppSettings.Settings.Ensure(AppSettingsKeys.GoogleClientSecret, viewModel.GoogleClientSecret);

            if (viewModel.Name.HasValue())
            {
                config.AppSettings.Settings.Ensure(AppSettingsKeys.Name, viewModel.Name);
            }

            //config.AppSettings.Settings.Ensure(AppSettingsKeys.Initialized, "True");

            config.Save();

            var authResult = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata(true)).AuthorizeAsync(cancellationToken);
            if (authResult.Credential != null)
            {
                // this should never happen, but...
                return RedirectToAction("Index", "Admin");
            }
            
            return Redirect(authResult.RedirectUri);
        }
    }
}