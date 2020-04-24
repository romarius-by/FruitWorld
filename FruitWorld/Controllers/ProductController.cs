using FruitWorld.Models;
using FruitWorld.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FruitWorld.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 2;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category==category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage-1)*PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count() // я сам пофиксил, надо смотреть что дальше
                },
                CurrentCategory = category
            });
    }
}
