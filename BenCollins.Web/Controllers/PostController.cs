using BenCollins.Web.Data;
using BenCollins.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BenCollins.Web.Extensions;
using BenCollins.Web.ViewModel;
using StackExchange.Profiling;

namespace BenCollins.Web.Controllers
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
            int? postId = postIdValue.HasValue() ? Convert.ToInt32(postIdValue) : (int?)null;

            if (postId.HasValue)
            {                
                post = _postRepository.FindById(postId.Value);
            }
            else
            {
                post = new Post { Draft = true };
            }

            string title = collection["Title"];
            post.Title = title;
            post.Body = collection["wmd-input"];
            post.Slug = title.AsSlug();

            if (postId.HasValue)
            {
                post.ModifiedDate = DateTime.UtcNow;
                _postRepository.Update(post);
            }
            else
            {
                _postRepository.Add(post);
            }
            
            return Json(post.Id);
        }

        //
        // GET: /Post/Edit/5
        [Route("post/edit/{id}")]
        public ViewResult Edit(int id)
        {
            var post = _postRepository.FindById(id);

            return View(post);
        }

        //
        // POST: /Post/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [Route("post/edit/{id}")]
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
        [Route("post/delete/{id}")]
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
            var breakIndex = post.Body.IndexOf("^^^");
            HtmlString excerptHtml = null, fullHtml = null;
            using (MiniProfiler.Current.Step("Markdown transform"))
            {
                var md = new MarkdownSharp.Markdown();
                if (options.HasFlag(PostViewModelOptions.Excerpt))
                {
                    excerptHtml = new HtmlString(md.Transform(post.Body.Substring(0, breakIndex > 0 ? breakIndex : post.Body.Length)));
                }

                if (options.HasFlag(PostViewModelOptions.FullBody))
                {
                    var bodyMd = post.Body.Remove(breakIndex, 3);
                    fullHtml = new HtmlString(md.Transform(bodyMd));
                }
            }

            return new PostViewModel
            {
                Id = post.Id.Value,
                Title = post.Title,
                CreationDate = post.CreationDate,
                ModifiedDate = post.ModifiedDate,
                BodyHtml = fullHtml,
                BodyExcerpt = excerptHtml,
                Excerpted = breakIndex > -1,
                Slug = post.Slug
            };
        }
    }
}
