namespace Market.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public int productCommentId { get; set; }
        public product productComment { get; set; }
    }
}
