using Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private MarketDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;

        public HomeController(MarketDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            CartItems();
            return View(new product().getProducts(db));
        }
        public IActionResult CartItems()
        {
            TempData["cart"] = new CartItem().ListOfItems(db, UserController.currentId );
            return View();
        }

        [HttpPost]
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       
     

    }
}