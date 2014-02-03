using BlogOne.Web.Data;
using System.Linq;
using System.Web.Mvc;

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
        [Route("home")]
        public ActionResult Index()
        {
            var posts = _postRepository.FindPublished(1, 5)
                .Select(p => PostController.ViewModelFromPost(p, PostController.PostViewModelOptions.Excerpt));
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}