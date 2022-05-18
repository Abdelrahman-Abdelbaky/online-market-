using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IshowAdminData
    {
        public IActionResult ShowAdmins();
        public void RenderAdminData(Admin model);
        public void getDataByID(int id);
    }
}
