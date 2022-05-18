using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.Interfaces
{
    public interface IProductModification
    {
        public IActionResult Addproduct();
        public  Task<IActionResult> addProductItem(product product);

        //async
        public IActionResult updateProduct(int id, product model);
        
        //async
        public Task<IActionResult> updateProductItem(product model);

        public IActionResult deleteProduct();
    }
}
