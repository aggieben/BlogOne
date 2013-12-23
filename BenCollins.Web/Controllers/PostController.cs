using BenCollins.Web.Data;
using BenCollins.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BenCollins.Web.Extensions;

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

        //
        // GET: /Post/
        [AllowAnonymous]
        [Route("posts")]
        public ActionResult Index()
        {
            return View();
        }

        ////
        //// GET: /Post/Details/5
        //[AllowAnonymous]
        //[Route("post/{id}")]
        //public ActionResult PostById(int id)
        //{
        //    var post = _postRepository.FindById(id);
        //    return RedirectToAction("Details", new { slug = post.Slug });
        //}

        [AllowAnonymous]
        [Route("post/{slug}")]
        public ActionResult Details(string slug)
        {
            var post = _postRepository.FindBySlug(slug);
            return View(post);
        }

        //
        // GET: /Post/Create
        [Route("post/new")]
        public ActionResult Create()
        {
            return View();
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
        public ActionResult Edit(int id)
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
        // GET: /Post/Delete/5
        [Route("post/delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Post/Delete/5
        [HttpPost]
        [Route("post/delete/{id}")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
