using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Miniatuurland.Models;
using Miniatuurland.Services;

namespace Miniatuurland.Controllers
{
    public class ProductController : Controller
    {
        private miniatuurlandEntities1 db = new miniatuurlandEntities1();
        private ProductService service = new ProductService();

        // geeft lijst van verschillende productlines
        public ActionResult Index()
        {
            if (TempData["loginError"] != null)
            {
                this.ModelState.AddModelError(String.Empty, TempData["loginError"].ToString());
                ViewBag.errorcount = ModelState.Values.Count();
            }
            List<ProductLine> productlines = service.GetProductLines();
            return View(productlines);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Product product = service.GetProductById((int)id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.productlijn = service.GetProductLineById((int)product.productlineID);
            return View(product);
        }

        //LIST: lijst van producten binnen een bepaald productline
        public ActionResult List(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            var productLijn = service.GetProductLineById((int)id);
            ViewBag.productline = productLijn;
            Session["productline"] = productLijn;
            if (productLijn == null)
            {
                return RedirectToAction("Index");
            }
            List<Product> productlijst = service.ListByProductLineId((int)id);
            return View(productlijst);
        }

        //ChildAction voor Layout: Navbar
        [ChildActionOnly]
        public ActionResult NavBar()
        {
            ViewBag.productline = Session["productline"];            
            Session["productline"] = null;
            var productLines = service.GetProductLines();
            return PartialView(productLines);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.productlineID = new SelectList(db.ProductLines, "productLineID", "productLine");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,product,scale,description,quantityInStock,quantityInOrder,buyPrice,productlineID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productlineID = new SelectList(db.ProductLines, "productLineID", "productLine", product.productlineID);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productlineID = new SelectList(db.ProductLines, "productLineID", "productLine", product.productlineID);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,product,scale,description,quantityInStock,quantityInOrder,buyPrice,productlineID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productlineID = new SelectList(db.ProductLines, "productLineID", "productLine", product.productlineID);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
