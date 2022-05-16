using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    public class product
    {
        public int Id { get; set; }
        public string name { get; set; }

        public int productCount { get; set; }

        public string description { get; set; }

        public int price { get; set; }  

        public DateTime insertTime { get; set; }

        public string imgSrc { get; set; }

        [NotMapped]
        public IFormFile img { get; set; }

        public int category { get; set; }

        public List<Comments> ProductComment { get; set; }



        public List<product> getProducts(MarketDBContext db)
        {
            return db.products.ToList();
        }


        public void addProduct(MarketDBContext db,product product)
        {
            db.products.Add(product);
            db.SaveChanges();
        }

        public void updateProduct(MarketDBContext db, product product)
        {
            db.products.Update(product);
            db.SaveChanges();
        }
        public product getProductByID(MarketDBContext db,int id)
        {
            return db.products.Find(id);
        }

        public void deleteProduct(MarketDBContext db, int id)
        {
            db.products.Remove(getProductByID(db, id));
            db.SaveChanges();
        }

        public void DecreaseProducts(MarketDBContext db,List<CartItem> productSelected)
        {
            foreach (var quantity in productSelected)
            {
                //var product = GetProduct(quantity.ProductId);
                var product = getProductByID( db,quantity.ProductId);
                if (product != null)
                {
                    product.productCount = product.productCount - quantity.Quantity;
                    new CartItem().SaveChanges(db);
                }
            }
            //}
        }

        public List<product> searchForProduct(string searchWord, MarketDBContext db)
        {

            return db.products.Where(c => c.name.Contains(searchWord)).ToList();

        }





    }
}
