using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore.Controllers
{
    public class ProductsController : Controller
    {
        private ProductRepo repo = null;

        public ProductsController()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            String connectionString = configuration.GetConnectionString("Database");
            repo = new ProductRepo(connectionString);
        }

        // GET: Products
        public IActionResult Index()
        {
            List<Product> products = repo.GetProducts();
            return View(products);
        }

        public IActionResult Search(string? keyword)
        {
            ViewData["keyword"] = keyword;
            if (keyword == null || keyword == string.Empty)
            {
                return View("Index", new List<Product>());
            }
            List<Product> products = repo.searchProductByName(keyword);
            int unitPrice;
            if (int.TryParse(keyword, out unitPrice))
            {
                products.AddRange(repo.searchProductByUnitPrice(unitPrice));
            }
            products.DistinctBy(p => p.ProductId);
            if (products == null || products.Count == 0)
            {
                return View("Index", new List<Product>());
            }
            return View("Index", products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || repo.GetProducts() == null)
            {
                return NotFound();
            }

            var product = repo.getProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            var session = HttpContext.Session;
            session.Clear();
            if (ModelState.IsValid)
            {
                if (product.CategoryId <= 0)
                {
                    session.SetString("CreateProductError", "Category ID must be greater than 0");
                }
                else if (product.Weight.Equals(string.Empty) || product.Weight.Any(c => char.IsLetter(c)))
                {
                    session.SetString("CreateProductError", "");
                }
                else if (product.UnitPrice.Equals(string.Empty))
                {
                    session.SetString("CreateProductError", "UnitInStock must be greater than 0");
                }
                else if (product.UnitInStock <= 0)
                {
                    session.SetString("CreateProductError", "UnitInStock must be greater than 0");
                }
                else
                {
                    repo.AddNewProduct(product);
                    repo.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Create));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || repo.GetProducts() == null)
            {
                return NotFound();
            }

            var product = repo.getProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var session = HttpContext.Session;
                    session.Clear();
                    if (ModelState.IsValid)
                    {
                        if (product.CategoryId <= 0)
                        {
                            session.SetString("EditProductError", "Category ID must be greater than 0");
                        }
                        else if (product.Weight.Equals(string.Empty) || product.Weight.Any(c => char.IsLetter(c)))
                        {
                            session.SetString("EditProductError", "");
                        }
                        else if (product.UnitPrice.Equals(string.Empty))
                        {
                            session.SetString("EditProductError", "UnitInStock must be greater than 0");
                        }
                        else if (product.UnitInStock <= 0)
                        {
                            session.SetString("EditProductError", "UnitInStock must be greater than 0");
                        }
                        else
                        {
                            repo.UpdateProductInfo(product);
                            repo.SaveChanges();
                            return RedirectToAction(nameof(Index));
                        }
                        return RedirectToAction(nameof(Edit));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || repo.GetProducts() == null)
            {
                return NotFound();
            }

            var product = repo.getProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (repo.GetProducts() == null)
            {
                return Problem("Entity set 'EStoreContext.Products'  is null.");
            }
            var product = repo.getProductById(id);
            if (product != null)
            {
                repo.DeleteProduct(product);
            }

            repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return repo.GetProducts().Any(p => p.ProductId == id);
        }
    }
}
