using My.AppStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace My.AppStore.Controllers
{
    [CartItemCalculator]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                var model = await entities.Categories.Select(
                    x => new CategoryModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        ID = x.ID,
                        Products = x.CategoriesProducts.Select(
                            y => new ProductModel
                            {
                                ID = y.ProductID,
                                Description = y.Product.Description,
                                Name = y.Product.Name,
                                Price = y.Product.Price,
                                Images = y.Product.ProductImages.Select(z => z.FilePath).Take(1)
                            }).Take(3)
                    }).ToArrayAsync();
                return View(model);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [OutputCache(Duration = 300)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
