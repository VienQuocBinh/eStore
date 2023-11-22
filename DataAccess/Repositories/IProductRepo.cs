using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IProductRepo
    {
        public List<Product> GetProducts();
        public Product GetProductByID(int id);
        public void AddNewProduct(Product product);
        public void UpdateProductInfo(Product product);
        public void DeleteProduct(Product product);
        public bool SaveChanges();
        public IEnumerable<Product> searchProductByUnitPrice(int unitPrice);
        public List<Product> searchProductByName(string name);
        public Product? getProductById(int? id);
    }
}
