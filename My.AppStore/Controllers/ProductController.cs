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

                    model.Reviews = product.Reviews.Select(x => new ReviewModel
                    {
                        UserEmail = x.Email,
                        ID = x.ID,
                        Rating = x.Rating,
                        Body = x.Body
                    }).ToArray();

                    return View(model);
                }

                //var review = entities.Reviews.Find(id);
                //if(review != null)
                //{
                //    ReviewModel model = new ReviewModel();
                //    model.ID = review.ID;
                //    model.UserEmail = review.Email;
                //    model.Rating = review.Rating;
                //    model.Body = review.Body;
                //}
            }

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