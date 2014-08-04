using BlogOne.Common.Extensions;
using BlogOne.Web.Data;
using BlogOne.Web.Extensions;
using BlogOne.Web.Model;
using BlogOne.Web.ViewModel;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogOne.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [AllowAnonymous]
        [Route("post/{slug}")]
        public ActionResult Details(string slug)
        {
            var post = _postRepository.FindBySlug(slug);
            if (post.Draft && !Request.IsAuthenticated)
            {
                return HttpNotFound();
            }
            
            return View(ViewModelFromPost(post));
        }

        //
        // GET: /Post/Create
        [Route("post/new")]
        public ActionResult Create()
        {
            return View(new Post());
        }

        //
        // POST: /Post/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("post/new")]
        public ActionResult Create(FormCollection collection)
        {
            string title = collection["Title"];
            var post = new Post
            {
                Title = title,
                Body = collection["wmd-input"],
                Slug = title.AsSlug(),
            };

            _postRepository.Add(post);

            return RedirectToAction("Details", new { slug = post.Slug });
        }
        
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("post/draft")]
        public JsonResult Draft(FormCollection collection)
        {
            Post post = null;
            var postIdValue = collection["PostId"];
            int? postId = postIdValue.HasValue() && postIdValue != String.Empty ? Convert.ToInt32(postIdValue) : (int?)null;

            if (postId.HasValue)
            {
                post = _postRepository.FindById(postId.Value);
            }
            else
            {
                post = new Post { Draft = true };
            }

            string title = collection["Title"];
            if (!String.IsNullOrWhiteSpace(title))
            {
                post.Title = title;
                post.Slug = title.AsSlug();
            }
            
            post.Subtitle = collection["Subtitle"];
            post.Body = collection["Body"];

            if (postId.HasValue)
            {
                post.ModifiedDate = DateTime.UtcNow;
                _postRepository.Update(post);
            }
            else
            {
                _postRepository.Add(post);
            }

            return Json(post.Shortcode);
        }

        [Route("post/{shortcode}/edit")]
        public ViewResult Edit(string shortcode)
        {
            var post = _postRepository.FindByShortcode(shortcode);
            
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("post/{shortcode}/publish")]
        public ActionResult Publish(string shortcode)
        {
            var post = _postRepository.FindByShortcode(shortcode);
            post.Draft = false;
            _postRepository.Update(post);

            return new EmptyResult();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("post/{shortcode}/edit")]
        public ActionResult Edit(string shortcode, FormCollection collection)
        {
            var post = _postRepository.FindByShortcode(shortcode);

            string title = collection["Title"];

            post.Title = title;
            post.Body = collection["wmd-input"];
            post.Slug = title.AsSlug();

            _postRepository.Update(post);

            return RedirectToAction("Details", new { slug = post.Slug });
        }        

        [HttpPost]
        [Route("post/{shortcode}/delete")]
        public ActionResult Delete(string shortcode)
        {
            var post = _postRepository.FindByShortcode(shortcode);
            _postRepository.Remove(post);

            return new EmptyResult();
        }

        [Flags]
        public enum PostViewModelOptions
        {
            Excerpt,
            FullBody
        }

        public static PostViewModel ViewModelFromPost(Post post, PostViewModelOptions options = PostViewModelOptions.Excerpt | PostViewModelOptions.FullBody)
        {
            return new PostViewModel
            {
                Id = post.Id.Value,
                Title = post.Title,
                Subtitle = post.Subtitle,
                CreationDate = post.CreationDate,
                ModifiedDate = post.ModifiedDate,
                BodyHtml = MvcHtmlString.Create(post.Body),
                Slug = post.Slug,
                Shortcode = post.Shortcode,
            };
        }
    }
}
