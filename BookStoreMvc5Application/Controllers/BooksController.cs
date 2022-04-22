using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;

using BookStoreMvc5Application.Models;
using BookStoreMvc5Application.ViewModels;

using X.PagedList;

namespace BookStoreMvc5Application.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Books
        public ActionResult Index(int? page, string search = null)
        {
            IQueryable<IndexViewModel> models = (db.Books
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ProjectTo<IndexViewModel>());

            if (!string.IsNullOrEmpty(search))
            {
                models = models.Where(x => x.AuthorName.Contains(search) ||
                x.CategoryName.Contains(search) ||
                x.Title.Contains(search)
                );

                ViewBag.search = search;
            }

            var pageNumber = page ?? 1;
            var onePageOfBooks = models.ToPagedList(pageNumber, 8);

            ViewBag.OnePageOfBooks = onePageOfBooks;

            return View(onePageOfBooks);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View(new CreateBookViewModel());
        }


        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);

            EditBookViewModel viewModel = AutoMapper.Mapper.Instance.Map<EditBookViewModel>(book);


            if (book == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,CreatedAt,Title,AuthorId,CategoryId,Pages,Cost")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        book.UpdatedAt = DateTime.Now;
        //        db.Entry(book).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(book);
        //}

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
