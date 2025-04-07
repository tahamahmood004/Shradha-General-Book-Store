using E_Project_.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace E_Project_.Controllers
{
    public class HomeController : Controller
    {
        Connection con = new Connection();



        public IActionResult Index()
        {
            var viewModel = new CombinedModel
            {
                Categories = con.Categories.ToList(),
                Products = con.products.Take(6).ToList()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ShopCategory(string categoryId)
        {
            List<Product> data;
            try
            {
                data = con.products.Where(p => p.CategoryId == categoryId).ToList();
            }

            catch (Exception ex)
            {
                data = new List<Product>();
            }

            return View(data);
        }
        public IActionResult ProductDetails(string productId)
        {
            var add_pro = con.products.Find(productId);

            if (add_pro == null)
            {
                return NotFound();
            }

            return View(add_pro);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Tracking()
        {
            return View();
        }
        public IActionResult ProductCheckout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProductCheckout(string First_name, string Last_name, string Phone, string Email, string Address, string City, string Postal_code, string Order_notee)
        {

            if (ModelState.IsValid)
            {
                string checkoutId = CheckoutId.GenerateCheckoutId();

                ProductCheckout pc = new ProductCheckout(checkoutId, First_name, Last_name, Phone, Email, Address, City, Postal_code, Order_notee);

                con.ProductCheckouts.Add(pc);
                con.SaveChanges();
                TempData["success"] = "Data insert successfully";

            }
            return View();
        }
        //public IActionResult ProductCheckout()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ProductCheckout(string First_name, string Last_name, string Phone, string Email, string Address, string City, string Postal_code, string Order_notee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            string checkoutId = CheckoutId.GenerateCheckoutId();


        //            con.ProductCheckouts.Add(checkoutId, First_name, Last_name, Phone, Email, Address, City, Postal_code, Order_notee);
        //            con.SaveChanges();
        //            TempData["success"] = "Data inserted successfully";
        //            return RedirectToAction("Success"); // Redirect to a success page or another action
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the error (consider using a logging framework)
        //            Console.WriteLine($"Error saving data: {ex.Message}");
        //            TempData["error"] = "An error occurred while saving data. Please try again.";
        //        }
        //    }

        //    // If the model state is invalid or an error occurred, return the view with the model
        //    return View(model);
        //}
        public IActionResult ShoppingCart()
        {
            return View();
        }
        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult Elements()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        public IActionResult SingleBlog()
        {
            return View();
        }
        public IActionResult Novel()
        {
            return View();
        }

        public IActionResult Magazine(/*string categoryId*/)
        {

            //if (string.IsNullOrEmpty(categoryId))
            //{
            //    return View(new List<Product>()); // Return empty if no category
            //}

            //var products = con.products.Where(p => p.CategoryId == categoryId).ToList();
            //return View(products); // Pass the filtered products to the view

            //List<Product> data;
            //try
            //{
            //    data = con.products.Where(p => p.CategoryId == categoryId).ToList();
            //}

            //catch (Exception ex)
            //{
            //    data = new List<Product>();
            //}

            //return View(data);

            return View();

        }

        public IActionResult Stationary()
        {
            return View();
        }

        public IActionResult  About_us()
        {
            return View();
        }
        public IActionResult Book()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
     


        [HttpPost]
        public IActionResult contact(string Message, string Name, string Email, string Subject)
        {

            if (ModelState.IsValid)
            {

                Contact ct = new Contact(0, Message, Name, Email, Subject);

                con.Contacts.Add(ct);
                con.SaveChanges();
                TempData["success"] = "Data insert successfully";

            }
            return View();
        }


            public IActionResult Edit(int Id)
            {
                var data = con.Contacts.Where(x => x.Id == Id).FirstOrDefault();
                return View(data);
            }

            [HttpPost]
            public IActionResult Edit(int Id, string Message, string Name, string Email, string Subject)
            {
                var data = con.Contacts.Where(x => x.Id == Id).FirstOrDefault();

                data.Message = Message;
                data.Name = Name;
                data.Email = Email;
            data.Subject = Subject;
            con.SaveChanges();

                return RedirectToAction("Showdata");
            }

        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Registration(string Name, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                Registration reg = new Registration(0, Name, Email, Password);
                con.Registrations.Add(reg);
                con.SaveChanges();
                TempData["success"] = "Data insert sucessfully";
            }
            return View();
        }
      
        [HttpPost]

        public IActionResult Login(string Email, string Password)
        {
            var log = con.Registrations.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (log == null)
            {
                TempData["login_fail"] = "invalid credentials";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("username", log.Name);
                TempData["success"] = "login sucessfully";
                return RedirectToAction("Index");
            }
           
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Feedback()
        {
            // Fetch existing feedbacks to display
            var FeedbackList = con.Feedbacks.ToList();
            ViewBag.FeedbackList = FeedbackList;
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(string Name, string Email, string Phone_number, string Review)
        {
            if (ModelState.IsValid)
            {
                // Save the new feedback to the database

                var Fb = new Feedback(0, Name, Email, Phone_number, Review);

                con.Feedbacks.Add(Fb);
                con.SaveChanges();
                TempData["success"] = "Data inserted successfully";

                // Clear the model state to reset the form
                ModelState.Clear();
            }

            // Fetch existing feedbacks to display
            var feedbackList = con.Feedbacks.ToList();
            ViewBag.FeedbackList = feedbackList;

            return View();
        }
        public IActionResult Payment_method()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Payment_method(string Card_name, string Card_number, string Exp_month, string Exp_year, string Cvv)
        {

            if (ModelState.IsValid)
            {

                Payment_method pm = new Payment_method(0, Card_name, Card_number, Exp_month, Exp_year, Cvv);

                con.Payment_methods.Add(pm);
                con.SaveChanges();
                TempData["success"] = "Data insert successfully";

            }
            return View();
        }

        //---------------------add to cart-------------------

        public IActionResult Cart(int productId)
        {
            var product = con.products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            // Retrieve cart from session
            var cartJson = HttpContext.Session.GetString("Cart");
            List<Product> cart = string.IsNullOrEmpty(cartJson) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(cartJson);

            // Check if the product is already in the cart
            var cartProduct = cart.FirstOrDefault(p => p.Id == productId);

            if (cartProduct != null)
            {
                cartProduct.Quantity++;  // Increase quantity
            }
            else
            {
                product.Quantity = 1;
                cart.Add(product);  // Add new product to cart
            }

            // Save the cart back to session
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(cartJson);
            return View(cart); // Return the cart to the view
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(string productId, int change)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(cartJson);
            var cartProduct = cart.FirstOrDefault(p => p.P_Id == productId);

            if (cartProduct != null)
            {
                cartProduct.Quantity += change;
                if (cartProduct.Quantity < 0) cartProduct.Quantity = 0; // Prevent negative quantities
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Json(cartProduct); // You can return the updated product if needed
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View(); // Just load the checkout view, cart items will be handled via JavaScript
        }

        [HttpPost]
        public ActionResult ConfirmPurchase(string name, string email, string cartItems)
        {
            var deserializedCartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartItems);


            // Save the order (name, email, and cart items) into the database
            // Example: SaveOrderToDatabase(name, email, deserializedCartItems);

            return RedirectToAction("OrderConfirmation");
        }

        public ActionResult OrderConfirmation()
        {
            return View(); // Display a confirmation message.
        }

    }

    }


