using Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private MarketDBContext db;
        public HomeController(MarketDBContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddAddmin()
        {
            return View();
        }
        public IActionResult Addmin()
        {
            return View();
        }
        public IActionResult AddminRegister()
        {
            return View();
        }
        public IActionResult AdminHomePage()
        {
           return View(db.admins.ToList());
        }

        public IActionResult Show()
        {
            return View(db.users.ToList());
        }

        public IActionResult Delete(int id)
        {
            var model= db.users.Find(id);
            db.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }
        

       [HttpPost]
        public IActionResult UserRegister(User model)
        {
            if (CheckRegister(model))
            {
                db.users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Register Error"]=model.Email +" is exits before";
                return RedirectToAction("Register");
            }
                
        }
        public Boolean CheckRegister(User model) 
        {
            if (!db.users.Any(x=>x.Email.Equals(model.Email)))
            {
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public IActionResult UserLogin(User model)
        {
            if (CheckUserEmail(model))
            {
                if (CheckUserPassword(model))
                {
                    RenderUserData(model);
                    return RedirectToAction("HomePage");
                }
                else
                {
                    TempData["LoginError"] = "Email or password is incorrect";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Something wrong was happened";
                return RedirectToAction("Index");
            }
               
        }
        public Boolean CheckUserEmail(User model)
        {
            if (db.users.Any(x => x.Email.Equals(model.Email)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean CheckUserPassword(User model)
        {
            if (db.users.Any(x => x.Password.Equals(model.Password)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public void RenderUserData(User model)
        {
            foreach(var item in db.users)
            {
                if (item.Email.Equals(model.Email))
                {
                    TempData["UserId"] = item.Id;
                    TempData["UserUsername"] = item.Username;
                    TempData["UserEmail"] = item.Email;
                }
            }
        }

        public IActionResult SaveFeedback(Feedback model)
        {
            db.feedbacks.Add(model);
            db.SaveChanges();
            TempData["SaveFeedback"] = "Your feedback is sent succefully";
            return RedirectToAction("HomePage");
        }








        [HttpPost]
        public IActionResult AdminRegister(Admin model)
        {
            if (CheckAdminRegister(model))
            {
                db.admins.Add(model);
                db.SaveChanges();
                return RedirectToAction("Addmin");
            }
            else
            {
                TempData["Register Error"] = model.Email + " is exits before";
                return RedirectToAction("Register");
            }

        }
        public Boolean CheckAdminRegister(Admin model)
        {
            if (!db.admins.Any(x => x.Email.Equals(model.Email)))
            {
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public IActionResult AdminLogin(Admin model)
        {
            if (CheckAdminEmail(model))
            {
                if (CheckAdminPassword(model))
                {
                    RenderAdminData(model);
                    return RedirectToAction("AdminHomePage");
                }
                else
                {
                    TempData["LoginError"] = "Email or password is incorrect";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Something wrong was happened";
                return RedirectToAction("Index");
            }

        }
        public Boolean CheckAdminEmail(Admin model)
        {
            if (db.admins.Any(x => x.Email.Equals(model.Email)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean CheckAdminPassword(Admin model)
        {
            if (db.admins.Any(x => x.Password.Equals(model.Password)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void RenderAdminData(Admin model)
        {
            foreach (var item in db.admins)
            {
                if (item.Email.Equals(model.Email))
                {
                    TempData["AdminId"] = item.Id;
                    TempData["AdminUsername"] = item.Username;
                    TempData["AdminEmail"] = item.Email;
                }
            }
        }
        
            
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}