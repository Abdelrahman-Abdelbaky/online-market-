namespace Market.Models
{
    public class Feedback
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }



        public void addFeedback(MarketDBContext db , Feedback feedback)
        {
            db.feedbacks.Add(feedback);
            db.SaveChanges();
        }

        public void deleteFeedback(MarketDBContext db, int id)
        {
            db.Remove(getFeedback(id,db));
            db.SaveChanges();
        }
        
        private Feedback getFeedback(int id, MarketDBContext db)
        {
            return db.feedbacks.Find(id);
        }
    }
}
