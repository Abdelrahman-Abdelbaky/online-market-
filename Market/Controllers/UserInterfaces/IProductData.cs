using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface IProductData
    {
        public void getDataByID(int id);
        public IActionResult ShowProduct(int? id, int? userId);
    }
}
