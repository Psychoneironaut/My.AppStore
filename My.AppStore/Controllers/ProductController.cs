using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.AppStore.Models;
using System.Diagnostics;

namespace My.AppStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [OutputCache(Duration = 300)]
        public ActionResult Index(int? id)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string cacheKey = "product_" + id;
            ProductModel model = ControllerContext.HttpContext.Cache.Get("product_" + id) as ProductModel;
            if(model == null)
            {
                using (AppStoreEntities entities = new AppStoreEntities())
                {
                    var product = entities.Products.Find(id);
                    if (product != null)
                    {
                        model.ID = product.ID;
                        model.Description = product.Description;
                        model.Name = product.Name;
                        model.Price = product.Price;
                        model.Images = product.ProductImages.Select(x => x.FilePath).ToArray();
                        //First two arguments control what gets added to the cache, everyhting else is the "cache policy"
                        ControllerContext.HttpContext.Cache.Add(cacheKey, model, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                        //TODO: Finish getting all the cache stuff setup; reference Joe's code as well as Slack if it's there
                    }
                }
            }

            timer.Stop();
            ViewBag.LoadTime = timer.ElapsedMilliseconds + " ms OR " + timer.ElapsedTicks + "ticks";
            return View(model);

            return HttpNotFound(string.Format("ID {0} Not Found", id));
        }

        // POST: Product
        [HttpPost]
        public ActionResult Index(ProductModel model)
        {
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    Order o = currentUser.Orders.FirstOrDefault(x => x.TimeCompleted == null);
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        currentUser.Orders.Add(o);
                    }
                    var product = o.OrdersProducts.FirstOrDefault(x => x.ProductID == model.ID);
                    if (product == null)
                    {
                        product = new OrdersProduct();
                        product.ProductID = model.ID ?? 0;
                        product.Quantity = 0;
                        o.OrdersProducts.Add(product);
                    }
                    product.Quantity += 1;
                }
                else
                {
                    Order o = null;
                    if (Request.Cookies.AllKeys.Contains("orderNumber"))
                    {
                        Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                        o = entities.Orders.FirstOrDefault(x => x.TimeCompleted == null && x.OrderNumber == orderNumber);

                    }
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        entities.Orders.Add(o);
                        Response.Cookies.Add(new HttpCookie("orderNumber", o.OrderNumber.ToString()));
                    }
                    var product = o.OrdersProducts.FirstOrDefault(x => x.ProductID == model.ID);
                    if (product == null)
                    {
                        product = new OrdersProduct();
                        product.ProductID = model.ID ?? 0;
                        product.Quantity = 0;
                        o.OrdersProducts.Add(product);
                    }
                    product.Quantity += 1;
                }

                entities.SaveChanges();
                TempData.Add("AddedToCart", true);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}