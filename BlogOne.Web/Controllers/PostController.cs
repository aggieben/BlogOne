using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlogOne.Web.Extensions;
using BlogOne.Web.Data;
using BlogOne.Web.Model;
using BlogOne.Web.ViewModel;
using StackExchange.Profiling;

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
        [Route("post/{id}/publish")]
        public ActionResult Publish(int id)
        {
            var post = _postRepository.FindById(id);
            post.Draft = false;
            _postRepository.Update(post);

            return new EmptyResult();
        }

        //
        // POST: /Post/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [Route("post/{id}/edit")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var post = _postRepository.FindById(id);

            string title = collection["Title"];

            post.Title = title;
            post.Body = collection["wmd-input"];
            post.Slug = title.AsSlug();

            _postRepository.Update(post);

            return RedirectToAction("Details", new { slug = post.Slug });
        }        

        //
        // POST: /Post/Delete/5
        [HttpPost]
        [Route("post/{id}/delete")]
        public ActionResult Delete(int id)
        {
            var post = _postRepository.FindById(id);
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
                CreationDate = post.CreationDate,
                ModifiedDate = post.ModifiedDate,
                BodyHtml = MvcHtmlString.Create(post.Body),
                Slug = post.Slug
            };
        }
    }
}
