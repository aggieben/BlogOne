using System.Configuration;
using System.Web.Configuration;
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
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("setup/submit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SetupSubmit(SetupViewModel viewModel)
        {
            return Content("OK");
        }
    }
}