using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IproductData
    {
        public IActionResult Index();

        public IActionResult productTable();
        public product SendDataTOform(product model);

    }
}
