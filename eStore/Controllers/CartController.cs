using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStore.Controllers
{
    public class CartController : Controller
    {
        public const string CARTKEY = "Cart";
        static ICartRepo cartRepo;
        IProductRepo productRepo;
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration["ConnectionStrings:Database"];
        }
        public CartController()
        {
            cartRepo = new CartRepo(GetConnectionString());
            productRepo = new ProductRepo(GetConnectionString());
        }
        public List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return cartRepo.GetCartItems();
        }
        public void SaveCartSession(List<CartItem> items)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(items);
            session.SetString(CARTKEY, jsoncart);
        }
        // GET: CartController
        public ActionResult ViewCart()
        {
            // get cart from session
            List<CartItem> cart = GetCartItems();
            return View(cart);
        }
        public IActionResult Add(int productId)
        {
            // do add
            if (cartRepo.AddItem(productId))
            {
                // get cart from repo then save to session
                SaveCartSession(cartRepo.GetCartItems());
                return RedirectToAction("ViewCart");
            }
            else
            {
                return NotFound("Not found product");
            }

        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int productId, int quantity)
        {
            var session = HttpContext.Session;
            if (quantity <= 0 || quantity > cartRepo.GetCartItem(productId).Product.UnitInStock)
            {
                session.SetString("EditCartError", $"Quantity must be greater than 0 and less than Unit In Stock");
            }
            else if (cartRepo.EditItem(productId, quantity))
            {
                SaveCartSession(cartRepo.GetCartItems());
                session.Clear();
            }
            else
            {
                session.SetString("EditCartError", $"Not found product");
            }
            return Redirect("/Cart/ViewCart");
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int productId, int quantity, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int productId)
        {
            // do remove
            if (cartRepo.RemoveItem(productId))
            {
                // get cart from repo then save to session
                SaveCartSession(cartRepo.GetCartItems());
                return RedirectToAction("ViewCart");
            }
            else
            {
                return NotFound("Not found this product");
            }


        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int productId, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Pay()
        {
            var session = HttpContext.Session;
            session.Clear();
            if (cartRepo.Pay())
            {
                session.SetString("PayMessage", $"Pay successfully ");
            }
            else
            {
                session.SetString("PayMessage", $"No item to pay");
            }
            return Redirect("/Order/Index");
        }
    }
}
