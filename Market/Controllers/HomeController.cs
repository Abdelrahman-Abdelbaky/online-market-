using Market.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private MarketDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;

        public HomeController(MarketDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
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
            return View(db.products.ToList());
        }

        public IActionResult Addproduct()
        {
            return View();
        }


        public IActionResult ShowProdect(int? id)
        {
            foreach (var item in db.products)
            {
                if (item.Id.Equals((int)id))
                {
                    TempData["productId"]=item.Id;
                    TempData["ShowProductName"] = item.name;
                    TempData["showProductPrice"] = item.price;
                    TempData["showProductCategory"] = item.category;
                    TempData["ShowproductCount"] = item.productCount;
                    TempData["ShowProductImgSrc"] = item.imgSrc;
                    TempData["showProductDescription"] = item.description;

                }
            }
            return View(id);
        }

        public IActionResult deleteProdect()
        {
            int id = (int)TempData["productId"];
            db.products.Remove(db.products.Find(id));
            db.SaveChanges();
            return RedirectToAction("productTable");
        }

        public void getDataByID(int id) {
            foreach (var item in db.products)
            {
                if (item.Id.Equals(id))
                {
                    TempData["productId"]=item.Id;
                    TempData["productName"] = item.name;
                    TempData["productPrice"] = item.price;
                    TempData["productCategory"] = item.category;
                    TempData["productCount"] = item.productCount;
                    TempData["productImgSrc"] = item.imgSrc;
                    TempData["productDescription"] = item.description;

                }
            }
        }
   

        public product SendDataTOform(product model) {
            model.name = (string)TempData["productName"];
            model.price = (int)TempData["productPrice"];
            model.category = (int)TempData["productCategory"];
            model.productCount = (int)TempData["productCount"];
            model.description = (string)TempData["productDescription"];
            model.imgSrc = (string)TempData["productImgSrc"];
            model.insertTime = DateTime.Now;
            return model;
        }

        public IActionResult updateProduct(int id,product model)
        {
            TempData["productId"]=id;
            getDataByID(id);
            model = SendDataTOform(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> updateProductItem(product model)
        {
            model.Id = (int)TempData["productId"];
         

            if (model.img != null) 
            {
                string Folder = "image/";
                Folder += Guid.NewGuid().ToString() + "_" + model.img.FileName;
                string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
                await model.img.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                model.imgSrc = Folder;
            }   
            db.products.Update(model);
            db.SaveChanges(); 
            return RedirectToAction("productTable");
        }



        [HttpPost]
        public async Task<IActionResult> addProductItem(product model)
        {
                model.insertTime = DateTime.Now;    
                string Folder = "image/";
                Folder += Guid.NewGuid().ToString() + "_" + model.img.FileName;
                string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
                await model.img.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                model.imgSrc = Folder;
                db.products.Add(model);
                db.SaveChanges();

            return RedirectToAction("Addproduct");
        }


        public IActionResult productTable()
        {
            return View(db.products.ToList());
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
        

        [HttpPost]
        public IActionResult AddFeedback(Feedback feedback)
        {
            db.feedbacks.Add(feedback);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }

        public async Task<IActionResult> Comments(product product,int id)
        {
            getDataByID(id);
            
            TempData["x"] = id;
            
            return View();
        }

        public IActionResult AddComments(Comments model)
        {
            model.productCommentId = (int)TempData["productId"];
            
            if (model.productCommentId != null)
            {
                db.comments.Add(model);
                db.SaveChanges();
                
                TempData["CommentMsg"]="Comment is sent successfully";
                return RedirectToAction("HomePage");
            }
            else
            {
                return RedirectToAction("Comments");
            }

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
                    return RedirectToAction("productTable");
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

        public IActionResult ShowFeedback()
        {
            return View(db.feedbacks.ToList());
        }

        public IActionResult DeleteFeedback(int id)
        {
            var model= db.feedbacks.Find(id);
            db.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ShowFeedback");
        }

        [HttpPost]
       
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}