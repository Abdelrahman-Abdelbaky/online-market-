using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public interface IAdminAuthentication
    {
        public IActionResult AdminRegister(Admin admin);
        public IActionResult AdminLogin(Admin model);

        public IActionResult AddminRegister();

    }
}
