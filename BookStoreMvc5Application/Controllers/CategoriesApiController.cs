using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStoreMvc5Application.Controllers
{
    public class CategoriesApiController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetCategoriesJson()
        {
            using (var db = new Models.BookStoreContext())
            {
                var allCategoriestQuery = from c in db.Categories
                                          orderby c.Name
                                          select new { c.Id, c.Name };

                var allCategories = await (allCategoriestQuery.ToListAsync());

                return Json(new
                {
                    categories = allCategories
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}