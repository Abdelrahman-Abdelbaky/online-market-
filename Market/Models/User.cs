namespace Market.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }


        public List<User> getUsers(MarketDBContext db)
        {
            return db.users.ToList();
        }

        public Boolean CheckRegister(User user, MarketDBContext db)
        {
            if (!db.users.Any(x => x.Email.Equals(user.Email)))
            {
                return true;
            }
            else
                return false;
        }
        public void addUser(User user, MarketDBContext db)
        {
            db.users.Add(user);
            db.SaveChanges();
        }

        private User getUserByID(MarketDBContext db,int id)
        {
            return db.users.Find(id);
        }

        public void deleteUser(int id, MarketDBContext db)
        {
            db.Remove(getUserByID( db, id));
            db.SaveChanges();
        }


    }
}
