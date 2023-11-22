using BusinessObject;

namespace DataAccess.Repositories
{
    public class ProductRepo : IProductRepo
    {
        public ProductRepo() { }
        public ProductRepo(String ConnectionString)
        {
            ProductDAO.ConnectionString = ConnectionString;
        }

        public void AddNewProduct(Product product)
        {
            ProductDAO.getInstance.AddNewProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            ProductDAO.getInstance.DeleteProduct(product);
        }

        public Product GetProductByID(int id)
        {
            return ProductDAO.getInstance.GetProductByID(id);
        }

        public Product? getProductById(int? id)
        {
            return ProductDAO.getInstance.GetProductByID(id);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.getInstance.GetProducts();
        }

        public bool SaveChanges()
        {
            return ProductDAO.getInstance.SaveChanges();
        }

        public List<Product> searchProductByName(string name)
        {
            return ProductDAO.getInstance.searchProductByName(name);
        }

        public IEnumerable<Product> searchProductByUnitPrice(int unitPrice)
        {
            return ProductDAO.getInstance.searchProductByUnitPrice(unitPrice);
        }

        public void UpdateProductInfo(Product product)
        {
            ProductDAO.getInstance.UpdateProduct(product);
        }
    }
}
