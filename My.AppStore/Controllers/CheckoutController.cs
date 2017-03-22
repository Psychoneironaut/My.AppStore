using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.AppStore.Models;
using System.Data.SqlClient;

namespace My.AppStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                using (AppStoreEntities entities = new AppStoreEntities())
                {
                    string uniqueName = Guid.NewGuid().ToString();
                    Order newOrder = new Order();
                    newOrder.BuyerEmail = uniqueName;

                    Address newShippingAddress = new Address();
                    // put in rest of address stuff

                    entities.Orders.Add(newOrder);
                    entities.SaveChanges();

                    int id = newOrder.ID;
                }
                //Validated
                //TODO: Persist this order to the database, redirect to a receipt page
                //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings("AppStore").ConnectionString;
                //    using (SqlConnection connection = new SqlConnection(connectionString))
                //    {
                //        connection.Open();
                //        try
                //        {
                //            string uniqueName = Guid.NewGuid().ToString();
                //            SqlCommand command = connection.CreateCommand();
                //            command.CommandText = string.Format("INSERT INTO [Order](ShipCareOf, PurchaserEmail) VALUES ('{0}', '{0}@codingtemple.com')", uniqueName);
                //            command.ExecuteNonQuery();

                //            SqlCommand command2 = connection.CreateCommand();
                //            command2.CommandText = string.Format("SELECT TOP 1 ID FROM [Order] WHERE ShipCareOf = '{0}'", uniqueName);
                //            int id = (int)command2.ExecuteScalar();

                //            SqlCommand command3 = connection.CreateCommand();
                //            command3.CommandType = System.Data.CommandType.StoredProcedure;
                //            command3.CommandText = "sp_CompleteOrder";
                //            SqlParameter p = new SqlParameter("orderID", System.Data.SqlDbType.Int, int.MaxValue, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Default, id);
                //            command3.Parameters.Add(p);

                //            command3.ExecuteNonQuery();
                //        }
                //        finally
                //        {
                //            connection.Close();
                //        }

                //    }
                //    //Validated
                //    //TODO: Persist this order to the database, redirect to a receipt page
                //    return RedirectToAction("Index", "Receipt");
                //}
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult States(string country)
        //{
        //    using(AppStoreEntities entities = new AppStoreEntities())
        //    {
        //        var c = entities.Countries
        //            .FirstOrDefault(x => x.Abbreviation == country);
        //        if (c != null)
        //        {
        //            return Json(c.States
        //                .Select(x => new StateModel {  ID}))
        //        }
        //    }
        //}

        [HttpPost]
        public ActionResult Countries()
        {
            using (AppStoreEntities entities = new AppStoreEntities())
            {
                return Json(entities.Countries.Select(x => new { Text = x.Name, Value = x.Abbreviation }).ToArray());
            }
        }
    }
}