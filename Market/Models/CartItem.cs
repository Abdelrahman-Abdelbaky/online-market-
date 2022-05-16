using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Market.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public product Product { get; set; }
        public int Quantity { get; set; }
        public int userId { get; set; }
        public CartItem CheckProductInCart(MarketDBContext db, int productId)
        {
            var product = db.cartItems.SingleOrDefault(x => x.ProductId == productId);
            return product;
        }
        public void SaveNewProduct(MarketDBContext db, CartItem cartItem)
        {
            db.cartItems.Add(cartItem);
            SaveChanges(db);
        }
        public void SaveChanges(MarketDBContext db)
        {
            db.SaveChanges();
        }
        public List<string> ListOfItems(MarketDBContext db,int currentId)
        {
            List<string> cartData = new List<string>();
            string temp = "";
            var cart = GetCartItems(db, currentId);
            foreach (var item in cart)
            {

                temp = item.Product.name + " " + item.Quantity;
                cartData.Add(temp);

            }
            return cartData;
        }
        public List<CartItem> GetCartItems(MarketDBContext db,int currentId)
        {
            var checkoutData = db.cartItems.Include(a => a.Product).Where(a => a.userId == currentId).ToList();
            return checkoutData;
        }
        public CartItem GetItem(MarketDBContext db,int productId)
        {
            var Item = db.cartItems.SingleOrDefault(a => a.ProductId == productId);
            return Item;
        }
        public List<CartItem> GetConfirmData(MarketDBContext db)
        {
            var confirmData = db.cartItems.Include(a => a.Product).ToList();
            return confirmData;
        }
        public void ClearCart(MarketDBContext db ,int currentId )
        {
            var catItems = GetCartItems(db,currentId);
            if (catItems != null)
            {
                foreach (var cartItem in catItems)
                {
                    db.cartItems.Remove(cartItem);
                    SaveChanges(db);
                }
            }
        }

    }
}
