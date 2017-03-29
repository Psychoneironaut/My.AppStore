using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.AppStore.Models;

namespace My.AppStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            CartModel model = new CartModel();
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                Order o = null;
                if (User.Identity.IsAuthenticated)
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    o = currentUser.Orders.FirstOrDefault(x => x.TimeCompleted == null);
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        currentUser.Orders.Add(o);
                        entities.SaveChanges();
                    }
                }
                else
                {
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
                        entities.SaveChanges();
                    }
                }

                model.Items = o.OrdersProducts.Select(x => new CartItemModel
                {
                    Product = new ProductModel
                    {
                        Description = x.Product.Description,
                        ID = x.Product.ID,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        Images = x.Product.ProductImages.Select(y => y.FilePath)
                    },
                    Quantity = x.Quantity
                }).ToArray();
                model.SubTotal = o.OrdersProducts.Sum(x => x.Product.Price * x.Quantity);
            }
            ViewBag.PageGenerationTime = DateTime.UtcNow;
            return View(model);
        }
    }
}