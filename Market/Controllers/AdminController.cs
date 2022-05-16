using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class AdminController : Controller
    {

        private MarketDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;

        public AdminController(MarketDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowAdmins()
        {
            return View(new Admin().GetAdmins(db));
        }

        public IActionResult DeleteAdmin(int id)
        {
            new Admin().deleteAdmin(db,id);
            return RedirectToAction("ShowAdmins");
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


        [HttpPost]
        public IActionResult AdminRegister(Admin admin)
        {
            if (CheckAdminRegister(admin))
            {
                new Admin().addAdmin(db, admin);
                return RedirectToAction("Addmin");
            }
            else
            {
                TempData["Register Error"] = admin.Email + " is exits before";
                return RedirectToAction("Register", "User");
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
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "Something wrong was happened";
                return RedirectToAction("Index", "Home");
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

        public IActionResult Addproduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> addProductItem(product product)
        {
            product.insertTime = DateTime.Now;
            string Folder = "image/";
            Folder += Guid.NewGuid().ToString() + "_" + product.img.FileName;
            string serverFolder = Path.Combine(_appEnvironment.WebRootPath, Folder);
            await product.img.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            product.imgSrc = Folder;

            new product().addProduct(db, product);

            return RedirectToAction("Addproduct");
        }

        public IActionResult productTable()
        {
            return View(new product().getProducts(db));
        }

        public void getDataByID(int id)
        {
            foreach (var item in db.products)
            {
                if (item.Id.Equals(id))
                {
                    TempData["productId"] = item.Id;
                    TempData["productName"] = item.name;
                    TempData["productPrice"] = item.price;
                    TempData["productCategory"] = item.category;
                    TempData["productCount"] = item.productCount;
                    TempData["productImgSrc"] = item.imgSrc;
                    TempData["productDescription"] = item.description;

                }
            }
        }
        

        public product SendDataTOform(product model)
        {
            model.name = (string)TempData["productName"];
            model.price = (int)TempData["productPrice"];
            model.category = (int)TempData["productCategory"];
            model.productCount = (int)TempData["productCount"];
            model.description = (string)TempData["productDescription"];
            model.imgSrc = (string)TempData["productImgSrc"];
            model.insertTime = DateTime.Now;
            return model;
        }



        public IActionResult updateProduct(int id, product model)
        {
            TempData["productId"] = id;
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

            new product().updateProduct(db, model);

            return RedirectToAction("productTable");
        }


        public IActionResult deleteProduct()
        {
            int id = (int)TempData["productId"];
            new product().deleteProduct(db, id);
            return RedirectToAction("productTable");
        }




        public IActionResult ShowFeedback()
        {
            return View(new Admin().GetFeedbacks(db));
        }

        public IActionResult DeleteFeedback(int id)
        {
            new Feedback().deleteFeedback(db, id);
            return RedirectToAction("ShowFeedback");

        }


        public IActionResult ShowComments()
        {
            //return View(new Admin().GetComments(db));

            return View(new Comments().ShowComment(db));
        }

        public IActionResult DeleteComment(int id)
        {
            new Comments().deleteComment(db, id);
            return RedirectToAction("ShowComments");

        }

        public IActionResult ShowUsers()
        {
            return View(new User().getUsers(db));
        }

        public IActionResult DeleteUser(int id)
        {
            new User().deleteUser(id, db);
            return RedirectToAction("ShowUsers");
        }
        public IActionResult SalesReport()
        {

            ViewData["ReportSales"] = new SalesReport().GetSalesData(db);
            return View();
        }

        public IActionResult AdminSearch(string searchWord)
        {
            var list = new product().searchForProduct(searchWord, db);

            return View(list);

        }

    }
}
