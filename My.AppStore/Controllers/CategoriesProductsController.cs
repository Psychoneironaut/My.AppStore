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
    public class CategoriesProductsController : Controller
    {
        private AppStoreEntities db = new AppStoreEntities();

        // GET: CategoriesProducts
        public ActionResult Index()
        {
            var categoriesProducts = db.CategoriesProducts.Include(c => c.Category).Include(c => c.Product);
            return View(categoriesProducts.ToList());
        }

        // GET: CategoriesProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesProduct categoriesProduct = db.CategoriesProducts.Find(id);
            if (categoriesProduct == null)
            {
                return HttpNotFound();
            }
            return View(categoriesProduct);
        }

        // GET: CategoriesProducts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            return View();
        }

        // POST: CategoriesProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,ProductID,Created,Modified")] CategoriesProduct categoriesProduct)
        {
            if (ModelState.IsValid)
            {
                db.CategoriesProducts.Add(categoriesProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", categoriesProduct.CategoryID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", categoriesProduct.ProductID);
            return View(categoriesProduct);
        }

        // GET: CategoriesProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesProduct categoriesProduct = db.CategoriesProducts.Find(id);
            if (categoriesProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", categoriesProduct.CategoryID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", categoriesProduct.ProductID);
            return View(categoriesProduct);
        }

        // POST: CategoriesProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,ProductID,Created,Modified")] CategoriesProduct categoriesProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriesProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", categoriesProduct.CategoryID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", categoriesProduct.ProductID);
            return View(categoriesProduct);
        }

        // GET: CategoriesProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesProduct categoriesProduct = db.CategoriesProducts.Find(id);
            if (categoriesProduct == null)
            {
                return HttpNotFound();
            }
            return View(categoriesProduct);
        }

        // POST: CategoriesProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriesProduct categoriesProduct = db.CategoriesProducts.Find(id);
            db.CategoriesProducts.Remove(categoriesProduct);
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
