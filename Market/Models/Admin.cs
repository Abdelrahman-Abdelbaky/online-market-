namespace Market.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }


        public void addAdmin(MarketDBContext db,Admin admin)
        {
            db.admins.Add(admin);
            db.SaveChanges();
        }
        public IEnumerable<Admin> GetAdmins(MarketDBContext db)
        {
            return db.admins.ToList();
        }

        public Admin getAdminByID(MarketDBContext db,int id)
        {
            return db.admins.Find(id);
        }

        public void deleteAdmin(MarketDBContext db,int id)
        {
            db.Remove(getAdminByID(db, id));
            db.SaveChanges();
        }


        public IEnumerable<Feedback> GetFeedbacks(MarketDBContext db)
        {
            return db.feedbacks.ToList();
        }

        public IEnumerable<Comments> GetComments(MarketDBContext db)
        {
            return db.comments.ToList();
        }


    }
}
