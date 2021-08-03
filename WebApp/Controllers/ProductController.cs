using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL _db;
        public ProductController(ProductDAL db)
        {
            _db = db;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var productModel = (from product in _db.GetAllProducts()
                                join cat in _db.GetAllCategories()
                            on product.CategoryId equals cat.CategoryId
                            select new ProductModel
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Description = product.Description,
                                UnitPrice = product.UnitPrice,
                                CategoryId = cat.CategoryId,
                                CategoryName = cat.Name
                            });
            return View(productModel);
        }

        
        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _db.GetAllCategories();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                ModelState.Remove("ProductId");
                if (ModelState.IsValid)
                {
                    _db.AddProduct(product);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.Categories = _db.GetAllCategories();
            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _db.GetAllCategories();
            Product product = _db.GetProductData(id);
            return View("Create", product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                _db.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                
            }
            ViewBag.Categories = _db.GetAllCategories();
            return View("Create", product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = _db.GetProductData(id);
                if(product != null)
                {
                    _db.DeleteProduct(id);
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
