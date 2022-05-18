using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IAdminModification
    {
        public IActionResult DeleteAdmin(int id);

        public IActionResult AddAddmin();
        public IActionResult Addmin();
    }
}
