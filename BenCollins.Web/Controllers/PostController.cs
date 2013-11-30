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
        //
        // GET: /Post/
        [AllowAnonymous]
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
        [Route("post/new")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
