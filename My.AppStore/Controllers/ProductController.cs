using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.AppStore.Models;

namespace My.AppStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [OutputCache(Duration = 300)]
        public ActionResult Index(int? id)
        {
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                var product = entities.Products.Find(id);
                if(product != null)
                {
                    ProductModel model = new ProductModel();
                    model.ID = product.ID;
                    model.Description = product.Description;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.Images = product.ProductImages.Select(x => x.FilePath).ToArray();

                    return View(model);
                }
            }

            return HttpNotFound(string.Format("ID {0} Not Found", id));
        }

        // POST: Product
        [HttpPost]
        public ActionResult Index(ProductModel model)
        {
            //TODO: Collect information about the selected product
            //persist it in some sort of "Cart/Basket/ShoppingBag" in a database
            List<ProductModel> cart = this.Session["Cart"] as List<ProductModel>;
            if (cart == null)
            {
                cart = new List<ProductModel>();
            }

            cart.Add(model);

            this.Session.Add("Cart", cart);

            TempData.Add("AddedToCart", true);

            return RedirectToAction("Index", "Cart");
        }
    }
}