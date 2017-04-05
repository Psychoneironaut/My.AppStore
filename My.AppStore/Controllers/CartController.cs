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
        private AppStoreEntities db = new AppStoreEntities();

        // GET: Cart
        public ActionResult Index()
        {
            ///////////
            CartModel model = new CartModel();
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                Order o = null;
                //OrdersProduct item = null;
                if (User.Identity.IsAuthenticated)
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    o = currentUser.Orders.FirstOrDefault(x => x.TimeCompleted == null);
                    //item = o.OrderProducts.FirstOrDefault(x => x.OrderID == o.ID);
                    if (o == null)
                    {
                        o = new Order();
                        o.OrderNumber = Guid.NewGuid();
                        currentUser.Orders.Add(o);
                        entities.SaveChanges();
                    }
                }
                ////////////
                else
                {
                    if (Request.Cookies.AllKeys.Contains("orderNumber"))
                    {
                        Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                        o = entities.Orders.FirstOrDefault(x => x.TimeCompleted == null && x.OrderNumber == orderNumber);
                        //item = o.OrderProducts.FirstOrDefault(x => x.OrderID == o.ID);
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
                ///////////
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
            //ViewBag.PageGenerationTime = DateTime.UtcNow;
            return View(model);
        }
        //////////

        public ActionResult RemoveItem(int? id)
        {
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                if (User.Identity.IsAuthenticated)
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    Order o = currentUser.Orders.FirstOrDefault(x => x.TimeCompleted == null);
                    OrdersProduct item = o.OrdersProducts.FirstOrDefault(x => x.ProductID == id);
                    if(item.Quantity == 1)
                    {   
                        item.Product.OrdersProducts = null;
                        if (o.OrdersProducts.Count == 1)
                        {
                            o.OrdersProducts = null;
                        }
                    }
                    else
                    {
                        item.Quantity -= 1;
                    }
                }
                else
                {
                    if (Request.Cookies.AllKeys.Contains("orderNumber"))
                    {
                        Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                        Order o = entities.Orders.FirstOrDefault(x => x.TimeCompleted == null && x.OrderNumber == orderNumber);
                        OrdersProduct item = o.OrdersProducts.FirstOrDefault(x => x.ProductID == id);
                        if (item.Quantity == 1)
                        {
                            item.Product.OrdersProducts = null;
                            if (o.OrdersProducts.Count == 1)
                            {
                                o.OrdersProducts = null;
                            }
                        }
                        else
                        {
                            item.Quantity -= 1;
                        }
                    }

                }
                entities.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}