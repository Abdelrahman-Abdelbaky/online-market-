namespace Market.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public int productCommentId { get; set; }
        public product productComment { get; set; }



        public void addComment(MarketDBContext db,Comments comment)
        {
            db.comments.Add(comment);
            db.SaveChanges();
        }



        public void deleteComment(MarketDBContext db, int id)
        {
            db.Remove(getComment(id,db));
            db.SaveChanges();
        }

        private Comments getComment(int id, MarketDBContext db)
        {
            return db.comments.Find(id);
        }

        private string getProductName(MarketDBContext db, int PID)
        {
            var productName = (from p in db.products where p.Id == PID select p.name).First();

            return productName;
        }


        private IEnumerable<ProductJoinComment> setCommentDetails(MarketDBContext db)
        {
            List<ProductJoinComment> list = new List<ProductJoinComment>();

            var comments = new Admin().GetComments(db);


            foreach (var item in comments)
            {

                ProductJoinComment ProductComment = new ProductJoinComment
                {
                    Product = new product(),
                    Comment = new Comments()

                };

                ProductComment.Comment = item;

                ProductComment.Product.Id = item.productCommentId;

                ProductComment.Product.name = getProductName(db, ProductComment.Product.Id);

                list.Add(ProductComment);

            }

            return list;

        }


        public IEnumerable<ProductJoinComment> ShowComment(MarketDBContext db)
        {

            return setCommentDetails(db);

        }
    }
}
