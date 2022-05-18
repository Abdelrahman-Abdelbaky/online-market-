using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public interface IAdminControl
    {
        public Boolean CheckAdminRegister(Admin model);
        public Boolean CheckAdminEmail(Admin model);
        public Boolean CheckAdminPassword(Admin model);
        public IActionResult AdminSearch(string searchWord);

    }
}
