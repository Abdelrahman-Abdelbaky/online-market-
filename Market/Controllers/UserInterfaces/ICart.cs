using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface ICart
    {
        public IActionResult AddItemToCart(int productId,int? id);
        public IActionResult CartItems();
        public IActionResult RemoveItem(int productId);
        public IActionResult DecreaseQuantity(int productId);
    }
}
