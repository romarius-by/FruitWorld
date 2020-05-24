using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitWorld.Models;
using Microsoft.AspNetCore.Authorization;

namespace FruitWorld.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductRepository _rep;
        public AdminController(IProductRepository rep)
        {
            _rep = rep;
        }
        public IActionResult Index()
        {
            return View(_rep.Products);
        }
        public ViewResult Edit(int productId) =>
            View(_rep.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _rep.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = _rep.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}