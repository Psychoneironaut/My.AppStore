using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using My.AppStore.Models;

namespace My.AppStore.Controllers
{
    public class ReviewsController : Controller
    {
        private AppStoreEntities db = new AppStoreEntities();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Product);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(string name, int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ProductName = name;
                //ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", review.ProductID);
                ViewBag.ProductID = id;
                return View(new Review());
            }
            TempData.Add("ReviewAttempted", true);
            //for when you try to redirect the usr to the create reviews
            //TempData.Add("ThisProductName", name);
            //TempData.Add("ThisProductID", id);
            return RedirectToAction("Login", "MyAccount");
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,Email,Rating,Body,Created,Modified")] Review review, int id)
        {
            if (ModelState.IsValid)
            {
                using (AppStoreEntities entities = new AppStoreEntities())
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        review.Created = DateTime.UtcNow;
                        review.Modified = DateTime.UtcNow;
                        review.ProductID = id;
                        AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                        review.Email = currentUser.Email;

                        db.Reviews.Add(review);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Product", new { id = id });

            //ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", review.ProductID);
            //return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", review.ProductID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,Email,Rating,Body,Created,Modified")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", review.ProductID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
