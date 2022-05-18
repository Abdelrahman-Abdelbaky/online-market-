using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface IUserData
    {
        public void RenderUserData(User model);
        public IActionResult UserSearch(string searchWord);
    }
}
