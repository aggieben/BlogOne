using BenCollins.Web.Data;
using BenCollins.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //
        // GET: /Post/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return View();
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
            // TODO: Add insert logic here
            var post = new Post
            {
                Title = collection["Title"],
                Body = collection["wmd-input"]
            };

            _postRepository.Add(post);

            return RedirectToAction("Details", new { id = post.Id });
        }
        
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Route("post/draft")]
        public ActionResult Draft(FormCollection collection)
        {
            return Content("draft");
        }

        //
        // GET: /Post/Edit/5
        [Route("post/edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Post/Edit/5
        [HttpPost]
        [Route("post/edit/{id}")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
