using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class UserController : Controller
    {

        private MarketDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;
        public static int currentId;
        public UserController(MarketDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UserRegister(User user)
        {
            if (new User().CheckRegister(user, db))
            {
                new User().addUser(user, db);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Register Error"] = user.Email + " is exits before";
                return RedirectToAction("Register");
            }

        }


        [HttpPost]
        public IActionResult UserLogin(User model)
        {
            if (CheckUserEmail(model))
            {
                if (CheckUserPassword(model))
                {
                    RenderUserData(model);
                    return RedirectToAction("HomePage","Home");
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
            foreach (var item in db.users)
            {
                if (item.Email.Equals(model.Email))
                {
                    currentId = item.Id;
                    TempData["UserId"] = item.Id;
                    TempData["UserUsername"] = item.Username;
                    TempData["UserEmail"] = item.Email;
                }
            }
        }


        //Done
        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFeedback(Feedback feedback)
        {
            new Feedback().addFeedback(db, feedback);
            return RedirectToAction("HomePage","Home");
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


        public async Task<IActionResult> Comments(product product, int id)
        {
            getDataByID(id);

            TempData["x"] = id;

            return View();
        }

        public IActionResult AddComments(Comments comment)
        {
            comment.productCommentId = (int)TempData["productId"];

            if (comment.productCommentId != null)
            {
                new Comments().addComment(db, comment);
                TempData["CommentMsg"] = "Comment is sent successfully";
                return RedirectToAction("HomePage","Home");
            }
            else
            {
                return RedirectToAction("Comments");
            }

        }

        public IActionResult ShowProduct(int? id)
        {
            foreach (var item in db.products)
            {
                if (item.Id.Equals((int)id))
                {
                    TempData["productId"] = item.Id;
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
        public IActionResult AddItemToCart(int productId)
        {
            if (productId > 0)
            {
                var product = new product().getProductByID(db, productId);
                var existProduct = new CartItem().CheckProductInCart( db,productId);
                if (product != null)
                {
                    if (existProduct == null)
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.ProductId = productId;
                        cartItem.Product = product;
                        cartItem.Quantity = 1;
                        cartItem.userId = currentId;
                        new CartItem().SaveNewProduct(db,cartItem);
                    }
                    else
                    {
                        int x = (int)existProduct.Quantity;
                        x++;
                        existProduct.Quantity = x;
                        new CartItem().SaveChanges(db);
                    }
                }

                CartItems();
            }
            return RedirectToAction("HomePage","Home");
        }
        public  IActionResult CartItems()
        {
            TempData["cart"] = new CartItem().ListOfItems( db,currentId );
            return View();
        }
        public IActionResult RemoveItem(int productId)
        {
            var Remove = new CartItem().GetItem(db,productId);
            if (Remove != null)
            {
                db.cartItems.Remove(Remove);
                new CartItem().SaveChanges(db);
                CartItems();
            }
            return RedirectToAction("HomePage","Home");
        }
        public IActionResult DecreaseQuantity(int productId)
        {
            var Decrease = new CartItem().GetItem(db,productId);
            if (Decrease != null)
            {
                int x = Decrease.Quantity;
                x--;
                Decrease.Quantity = x;
                CheckZeroQuantity(Decrease.Quantity, productId);
                new CartItem().SaveChanges(db);

            }
            return RedirectToAction("HomePage","Home");
        }
        public void CheckZeroQuantity(int Quantity, int productId)
        {
            if (Quantity == 0)
            {
                RemoveItem(productId);
            }
        }
        public IActionResult Checkout(int userId)
        {
            ViewData["checkout"] = new CartItem().GetCartItems(db,currentId);
            return View();
        }
        public IActionResult CheckoutDetails(int userId)
        {
            ViewData["checkoutDetail"] = new CartItem().GetCartItems(db, currentId);
            return View();
        }
        public IActionResult Confirm()
        {
            var ConfirmData =new CartItem().GetConfirmData(db);
            new SalesReport().SaveSalesData(db,ConfirmData,currentId);
            new product().DecreaseProducts(db,ConfirmData);
            new CartItem().ClearCart( db, currentId);
            return RedirectToAction("HomePage","Home");
        }
        public IActionResult UserSearch(string searchWord)
        {
            var list = new product().searchForProduct(searchWord, db);

            return View(list);

        }


    }
}
