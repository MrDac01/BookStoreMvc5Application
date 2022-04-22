using BookStoreMvc5Application.Models;
using BookStoreMvc5Application.ViewModels.Authors;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStoreMvc5Application.Controllers
{
    public class AuthorApiController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthorJson([Bind(Include = "FirstName,LastName")] CreateAuthorsViewModel createAuthorsViewModel) 
        {
            if (ModelState.IsValid)
            { 
                using (var db = new Models.BookStoreContext())
                {
                    Author author = db.Authors.Create();
                    author.CreatedAt = DateTime.Now;
                    author.FirstName = createAuthorsViewModel.FirstName;
                    author.LastName = createAuthorsViewModel.LastName;

                    db.Authors.Add(author);
                    db.SaveChanges();
                }

                return Json(new { status = true });
            }
                return Json(new { status = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAuthorJson([Bind(Include = "Id,FirstName,LastName")] EditAuthorsViewModel editAuthorsViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Models.BookStoreContext())
                {
                    Author author = db.Authors.Find(editAuthorsViewModel.Id);
                    author.FirstName = editAuthorsViewModel.FirstName;
                    author.LastName = editAuthorsViewModel.LastName;
                    author.UpdatedAt = DateTime.Now;
                    //db.Entry(author).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Json(new { status = true });
            }

            return Json(new { status = false });
        }
    }
}