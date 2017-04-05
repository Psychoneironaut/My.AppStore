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
        AppStoreEntities entities = new AppStoreEntities();
        OrderContainerModel ModelList = new OrderContainerModel();
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
        
        //HOPEFULLY THIS WILL HELP MAKE THE IMAGE ACT AS A BUTTON WITH A HYPERLINK, MIGHT NOT HELP WITH PUTTNIG TEXT OVER AN IMAGE THOUGH, COULD DO THAT IN GIMP MANUALLY
        //public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        //{
        //    var url = new UrlHelper(html.ViewContext.RequestContext);

        //    // build the <img> tag
        //    var imgBuilder = new TagBuilder("img");
        //    imgBuilder.MergeAttribute("src", url.Content(imagePath));
        //    imgBuilder.MergeAttribute("alt", alt);
        //    string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

        //    // build the <a> tag
        //    var anchorBuilder = new TagBuilder("a");
        //    anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
        //    anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
        //    string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

        //    return MvcHtmlString.Create(anchorHtml);
        //}

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

        [ActionName("Profile")]
        public ActionResult ProfileAction()
        {
            if (User.Identity.IsAuthenticated)
            {
                AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);


                ModelList.Orders = currentUser.Orders.Select(x => new OrderUserModel
                {
                    Order = new OrderModel
                    {
                        Id = x.ID,
                        //ShippingAddress = x.Address.ShippingAddress1,
                        EmailUsed = x.BuyerEmail,


                        Products = x.OrdersProducts.Select(y => new OrdersProduct
                        {
                            Product = new Product
                            {
                                Name = y.Product.Name,
                                Price = y.Product.Price
                            },
                            Quantity = y.Quantity,
                        })
                    },
                }).ToArray();
            }
            ViewBag.Message = "Your Profile";
            return View(ModelList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
