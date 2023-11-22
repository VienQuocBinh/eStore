using BusinessObject;

namespace DataAccess;

public class ProductDAO : BaseDAO
{
    private static ProductDAO Instance = null;
    private ProductDAO() { }
    public static ProductDAO getInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new ProductDAO();
            }
            return Instance;
        }
    }

    public List<Product> GetProducts()
    {
        return context.Products.ToList();
    }
    public Product GetProductByID(int id)
    {
        return context.Products.Find(id);
    }
    public Product GetProductByID(int? id)
    {
        return context.Products.Find(id);
    }
    public void AddNewProduct(Product product)
    {
        context.Products.Add(product);
    }

    public List<Product> searchProductByName(string keyword)
    {
        return context.Products.Where(p => p.ProductName.Contains(keyword)).ToList();
    }

    public void UpdateProduct(Product product)
    {
        var clone = context.Products.First(p => p.ProductId == product.ProductId);
        if (clone != null)
        {
            clone.ProductId = product.ProductId;
            clone.ProductName = product.ProductName;
            clone.CategoryId = product.CategoryId;
            clone.Weight = product.Weight;
            clone.UnitPrice = product.UnitPrice;
            clone.UnitInStock = product.UnitInStock;
            context.Products.Update(clone);
        }
    }

    internal IEnumerable<Product> searchProductByUnitPrice(int unitPrice)
    {
        return context.Products.Where(p => p.UnitPrice == unitPrice);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public Product? GetProductById(int? id)
    {
        return context.Products.FirstOrDefault(p => p.ProductId == id);
    }
}
