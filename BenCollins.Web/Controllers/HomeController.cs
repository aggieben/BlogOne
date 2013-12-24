using BenCollins.Web.Data;
using BenCollins.Web.ViewModel;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenCollins.Web.Controllers
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