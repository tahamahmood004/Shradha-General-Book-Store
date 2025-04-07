using E_Project_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace E_Project_.Controllers
{
    public class DashboardController : Controller
    {
        Connection con = new Connection();



        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return View();
            }
            return RedirectToAction("login");
        }


        public IActionResult Privacy()
        {

            return View();
        }
        public IActionResult login()
        {

            return View();
        }
     

     
        [HttpPost]
        public IActionResult login(string Email, string Password)
        {
            // Check if the credentials match the admin's
            if (Email == "admin@gmail.com" && Password == "123456")
            {

                // Redirect to the dashboard
                return View("Index", "Dashboard");
            }

            // If credentials do not match, return to the login view
            TempData["fail"] = "Invild Credentails"; // Optional: Display an error message
            return View();
        }


        public IActionResult logout()
        {

            return RedirectToAction("login");
        }
        //if (ModelState.IsValid)
        //{
        //    if (Email == "admin@gmail.com" && Password == "12345")
        //    {

        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid login attempt.");
        //    }
        //}
        //return View();



        //----------------Category---------------------

        public IActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Category(string Name, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                var img_name = Image.FileName;
                var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img_name);

                using (FileStream Stream = new FileStream(img_path, FileMode.Create))
                {
                    Image.CopyTo(Stream);
                }
                string categoryId = CategoryId.GenerateCategoryId();


                Category ct = new Category(categoryId, Name, img_name);
                con.Categories.Add(ct);
                con.SaveChanges();
                TempData["success"] = "Data insert Successfully";
            }
            return RedirectToAction("Category_Showdata");
        }

        public IActionResult Category_Showdata()
        {
            List<Category> data;
            try
            {
                data = con.Categories.ToList();
            }

            catch (Exception ex)
            {
                data = new List<Category>();
            }

            return View(data);
        }

        public IActionResult Category_Edit(string Id)
        {

            var data = con.Categories.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Category_Edit(string Id, string Name, IFormFile Image)
        {
            var image_name = Image.FileName;
            var data = con.Categories.Where(x => x.Id == Id).FirstOrDefault();

            data.Image = image_name;
            data.Name = Name;

            var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image_name);

            // Save the image to the server
            using (FileStream stream = new FileStream(img_path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            con.SaveChanges();

            return RedirectToAction("Category_Showdata");
        }

        public IActionResult Category_delete(string Id)
        {

            var data = con.Categories.Where(x => x.Id == Id).FirstOrDefault();

            con.Categories.Remove(data);
            con.SaveChanges();

            return RedirectToAction("Category_Showdata");
        }

        //----------------Category End---------------------
    
public IActionResult Product()
        {
            var categories = con.Categories.ToList(); // Fetch categories
            ViewBag.Categories = new SelectList(categories, "Id", "Name"); // Create SelectList
            return View();
        }


        [HttpPost]
        public IActionResult Product(IFormFile Image, string Writter_Name, string Book_Name, int Stock, int Price, string CategoryId)
        {
            string img_name = null; // Initialize img_name

            // Validate the model state
            if (ModelState.IsValid)
            {
                // Validate the uploaded image
                if (Image != null && Image.Length > 0)
                {
                    img_name = Image.FileName; // Set img_name if the image is valid
                    var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img_name);

                    // Save the image to the server
                    using (FileStream stream = new FileStream(img_path, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please upload an image.");
                }

                // Check if the book name or image already exists in the database within the same category
                var existingProduct = con.products
                    .FirstOrDefault(p =>
                        p.CategoryId == CategoryId && // Check within the same category
                        ((p.Book_Name != null && p.Book_Name.ToLower() == Book_Name.ToLower()) ||
                        (p.Image != null && p.Image.ToLower() == img_name.ToLower()))
                    );

                // Add specific error messages if the book or image already exists
                if (existingProduct != null)
                {
                    if (existingProduct.Book_Name != null && existingProduct.Book_Name.ToLower() == Book_Name.ToLower())
                    {
                        ModelState.AddModelError("Book_Name", "This book name already exists in the selected category.");
                    }

                    if (existingProduct.Image != null && existingProduct.Image.ToLower() == img_name?.ToLower())
                    {
                        ModelState.AddModelError("Image", "This image already exists in the selected category.");
                    }

                    // Return the view with the model state errors
                    var categories = con.Categories.ToList();
                    ViewBag.Categories = new SelectList(categories, "Id", "Name");
                    return View();
                }

                //Generate Alphanumeric Id
                string productId = IdGenerator.GenerateAlphanumericId();

                // Create and save the new product if no conflicts were found
                Product pro = new Product(productId, img_name, Writter_Name, Book_Name, Stock, Price, CategoryId);
                con.products.Add(pro);
                con.SaveChanges();
                TempData["success"] = "Data inserted successfully.";
            }

            // Fetch categories if model state is invalid or on initial GET
            var categoriesList = con.Categories.ToList();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");

            return RedirectToAction("Product_data");
        }

        public IActionResult Product_Edit(int Id)
        {
            // Fetch categories if model state is invalid or on initial GET
            var categoriesList = con.Categories.ToList();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");

            var data = con.products.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Product_Edit(int Id, IFormFile Image, string Writter_Name, string Book_Name, int Stock, int Price, string CategoryId)
        {
            var image_name = Image.FileName;
            var data = con.products.Where(x => x.Id == Id).FirstOrDefault();

            data.Image = image_name;
            data.Writter_Name = Writter_Name;
            data.Book_Name = Book_Name;
            data.Stock = Stock;
            data.Price = Price;
            data.CategoryId = CategoryId;

            var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image_name);

            // Save the image to the server
            using (FileStream stream = new FileStream(img_path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            // Fetch categories if model state is invalid or on initial GET
            var categoriesList = con.Categories.ToList();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");

            con.SaveChanges();

            return RedirectToAction("Product_data");
        }

        public IActionResult Product_delete(int Id)
        {

            var data = con.products.Where(x => x.Id == Id).FirstOrDefault();

            con.products.Remove(data);
            con.SaveChanges();

            return RedirectToAction("Product_data");
        }


        //[HttpPost]
        //public IActionResult Product(IFormFile Image, string Writter_Name, string Book_Name, string Stock, string Price, int CategoryId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Validate the uploaded image
        //        if (Image != null && Image.Length > 0)
        //        {
        //            var img_name = Image.FileName;
        //            var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img_name);

        //            using (FileStream stream = new FileStream(img_path, FileMode.Create))
        //            {
        //                Image.CopyTo(stream);
        //            }

        //            // Check if the book name or image already exists in the database
        //            var existingProduct = con.products
        //                .FirstOrDefault(p =>
        //                    (p.Book_Name != null && p.Book_Name.ToLower() == Book_Name.ToLower()) ||
        //                    (p.Image != null && p.Image.ToLower() == img_name.ToLower())
        //                );

        //            // Add specific error messages if the book or image already exists
        //            if (existingProduct != null)
        //            {
        //                if (existingProduct.Book_Name.ToLower() == Book_Name.ToLower())
        //                {
        //                    ModelState.AddModelError("Book_Name", "This book name already exists.");
        //                }

        //                if (existingProduct.Image.ToLower() == img_name.ToLower())
        //                {
        //                    ModelState.AddModelError("Image", "This image already exists.");
        //                }

        //                // Return the view with the model state errors
        //                var categories = con.Categories.ToList();
        //                ViewBag.Categories = new SelectList(categories, "Id", "Name");
        //                return View();
        //            }

        //            // Create and save the new product if no conflicts were found
        //            Product pro = new Product(0, img_name, Writter_Name, Book_Name, Stock, Price, CategoryId);
        //            con.products.Add(pro);
        //            con.SaveChanges();
        //            TempData["success"] = "Data inserted successfully.";
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Image", "Please upload an image.");
        //        }
        //    }

        //    // Fetch categories if model state is invalid or on initial GET
        //    var categoriesList = con.Categories.ToList();
        //    ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");

        //    return View();
        //}



        public IActionResult Product_data()
        {

            List<Product> data;

            try
            {
                data = con.products.ToList();
            }
            catch (Exception ex)
            {
                data = new List<Product>();
                // Optionally log the exception here
            }

            // Fetch categories to avoid multiple database calls in the view
            var categories = con.Categories.ToList();

            // Pass both products and categories to the view using ViewBag
            ViewBag.Categories = categories;

            return View(data);
        }
        //public IActionResult Product()
        //{
        //    ViewBag.Stock = new List<string>() { "In stock", "Out Stock" };

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Product(IFormFile image, string Writter_Name, string Book_Name, string Stock, string Price)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Ensure the context is available
        //        if (con == null)
        //        {
        //            ModelState.AddModelError("", "Database connection is not available.");
        //            ViewBag.Stock = new List<string>() { "In stock", "Out Stock" };
        //            return View();
        //        }

        //        // Always set the ViewBag.Stock before returning the view
        //        ViewBag.Stock = new List<string>() { "In stock", "Out Stock" };

        //        string imageName = image?.FileName;

        //        // Check if the book name or image already exists
        //        var existingProduct = con.Products
        //            .FirstOrDefault(p =>
        //                p.Book_Name.ToLower() == Book_Name.ToLower() ||
        //                (imageName != null && p.image.ToLower() == imageName.ToLower())
        //            );

        //        // Specific conditions for error messages
        //        if (existingProduct != null)
        //        {
        //            if (existingProduct.Book_Name.Equals(Book_Name, StringComparison.OrdinalIgnoreCase))
        //            {
        //                ModelState.AddModelError("Book_Name", "This book name already exists.");
        //            }

        //            if (imageName != null && existingProduct.image.Equals(imageName, StringComparison.OrdinalIgnoreCase))
        //            {
        //                ModelState.AddModelError("image", "This image already exists.");
        //            }

        //            // If both exist, add both errors
        //            return View();
        //        }

        //        // Handle image file upload if it's not null
        //        string img_name = null;
        //        if (image != null)
        //        {
        //            img_name = Path.GetFileName(image.FileName);
        //            var img_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", img_name);

        //            using (FileStream stream = new FileStream(img_path, FileMode.Create))
        //            {
        //                image.CopyTo(stream);
        //            }
        //        }

        //        // Generate a unique string ID for the product
        //        string productId = IdGenerator.GenerateAlphanumericId();

        //        // Create the Product object
        //        Product pt = new Product(productId, img_name, Writter_Name, Book_Name, Stock, Price);

        //        // Add product to the database
        //        con.Products.Add(pt);
        //        con.SaveChanges();

        //        TempData["success"] = "Data inserted successfully";
        //        return RedirectToAction("Product");
        //    }

        //    return View();
        //}


        //public IActionResult Novel_data()
        //{
        //    List<Product> data;
        //    try
        //    {
        //        data = con.Products.ToList();
        //    }

        //    catch (Exception ex)
        //    {
        //        data = new List<Product>();
        //    }

        //    return View(data);
        //}

        //public IActionResult Novel_Edit(string Id)
        //{
        //    var data = con.Products.Where(x => x.Id == Id).FirstOrDefault();
        //    return View(data);
        //}

        //[HttpPost]
        //public IActionResult Novel_Edit(string Id, string image, string Writter_Name, string Book_Name , string Price)
        //{
        //    var data = con.Products.Where(x => x.Id == Id).FirstOrDefault();

        //    data.image = image;
        //    data.Writter_Name = Writter_Name;
        //    data.Book_Name = Book_Name;
        //    data.Price = Price;
        //    con.SaveChanges();

        //    return RedirectToAction("Novel_data");
        //}



        public IActionResult Contact_showdata()
        {
            List<Contact> data;
            try
            {
                data = con.Contacts.ToList();
            }

            catch (Exception ex)
            {
                data = new List<Contact>();
            }

            return View(data);
        }
        public IActionResult Contact_delete(int Id)
        {

            var data = con.Contacts.Where(x => x.Id == Id).FirstOrDefault();

            con.Contacts.Remove(data);
            con.SaveChanges();

            return RedirectToAction("Contact_showdata");
        }
        public IActionResult Registration_showdata()
        {
            List<Registration> data;
            try
            {
                data = con.Registrations.ToList();
            }

            catch (Exception ex)
            {
                data = new List<Registration>();
            }

            return View(data);
        }
        public IActionResult Registration_delete(int Id)
        {

            var data = con.Registrations.Where(x => x.Id == Id).FirstOrDefault();

            con.Registrations.Remove(data);
            con.SaveChanges();

            return RedirectToAction(" Registration_showdata");
        }


        public IActionResult Feedback()
        {
            List<Feedback> data;
            try
            {
                data = con.Feedbacks.ToList();
            }

            catch (Exception ex)
            {
                data = new List<Feedback>();
            }

            return View(data);
        }

        public IActionResult Feedback_delete(int Id)
        {

            var data = con.Feedbacks.Where(x => x.Id == Id).FirstOrDefault();

            con.Feedbacks.Remove(data);
            con.SaveChanges();

            return RedirectToAction("Feedback");
        }


        public IActionResult ProductCheckout_showdata()
        {
            List<ProductCheckout> data;
            try
            {
                data = con.ProductCheckouts.ToList();
            }

            catch (Exception ex)
            {
                data = new List<ProductCheckout>();
            }

            return View(data);
        }

        public IActionResult Reply()
        {
            // Fetch existing feedbacks to display
            var FeedbackList = con.Feedbacks.ToList();
            ViewBag.FeedbackList = FeedbackList;
            return View();

        }
        [HttpPost]
        public IActionResult Reply(string Name, string Email, string Phone_number, string Review)
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

        public IActionResult ProductCheckout_delete(string Id)
        {

            var data = con.ProductCheckouts.Where(x => x.Id == Id).FirstOrDefault();

            con.ProductCheckouts.Remove(data);
            con.SaveChanges();

            return RedirectToAction("ProductCheckout_showdata");
        }

    }


}
