using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using BookStoreMvc5Application.Models;
using BookStoreMvc5Application.ViewModels.Authors;

namespace BookStoreMvc5Application.Controllers
{
    public class AuthorsEditController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: AuthorsEdit
        public ActionResult Index(string search = null)
        {
            IQueryable<IndexAuthorsViewModel> models = (db.Authors
                .ProjectTo<IndexAuthorsViewModel>());

            if (!string.IsNullOrEmpty(search))  
            {
                models = models.Where(x => x.FirstName.Contains(search) ||
                x.LastName.Contains(search)
                );

                ViewBag.search = search;
            }

            return View(models);
        }

        // GET: AuthorsEdit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: AuthorsEdit/Create
        public ActionResult Create()
        {
            return View(new CreateAuthorsViewModel());
        }

        //POST: AuthorsEdit/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName")] CreateAuthorsViewModel createAuthorsViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = db.Authors.Create();
                author.CreatedAt = DateTime.Now;
                author.FirstName = createAuthorsViewModel.FirstName;
                author.LastName = createAuthorsViewModel.LastName;

                db.Authors.Add(author);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        
            return View(createAuthorsViewModel);
        }

        // GET: AuthorsEdit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            EditAuthorsViewModel viewAuthorsModel = AutoMapper.Mapper.Instance.Map<EditAuthorsViewModel>(author);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(viewAuthorsModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] EditAuthorsViewModel editAuthorsViewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = db.Authors.Find(editAuthorsViewModel.Id);
                author.FirstName = editAuthorsViewModel.FirstName;
                author.LastName = editAuthorsViewModel.LastName;
                author.UpdatedAt = DateTime.Now;
                //db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editAuthorsViewModel);
        }

        // GET: AuthorsEdit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: AuthorsEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
