using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitWorld.Models;

namespace FruitWorld.Controllers
{
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
    }
}