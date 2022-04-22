using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BookStoreMvc5Application.Models;

namespace BookStoreMvc5Application.Controllers
{
    public class BookApiController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBookJson([Bind(Include = "Id,Title,AuthorId,CategoryId,Pages,Cost")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.CreatedAt = DateTime.Now;
                book.UpdatedAt = null;

                using (var db = new Models.BookStoreContext())
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                }

                return Json(new { status = true });
            }

            return Json(new { status = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBookJson([Bind(Include = "Id,CreatedAt,Title,AuthorId,CategoryId,Pages,Cost")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.UpdatedAt = DateTime.Now;

                using(var db = new Models.BookStoreContext())
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Json(new { status = true });
            }

            return Json(new { status = false });
        }
    }
}