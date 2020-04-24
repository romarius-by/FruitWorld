using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FruitWorld.Models;

namespace FruitWorld.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p));
        }
    }
}
