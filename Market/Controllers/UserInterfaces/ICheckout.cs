using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers.UserInterfaces
{
    public interface ICheckout
    {
        public void CheckZeroQuantity(int Quantity, int productId);
        public IActionResult Checkout(int? Id);
        public IActionResult CheckoutDetails(int? Id);
        public IActionResult Confirm(int? Id);

    }
}
