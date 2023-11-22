using BusinessObject;

namespace DataAccess.Repositories
{
    public class CartRepo : ICartRepo
    {
        IProductRepo productRepo;
        IOrderDetailRepo orderDetailRepo;
        IOrderRepo orderRepo;
        static List<CartItem> cart = new List<CartItem>();
        public CartRepo()
        {

        }
        public CartRepo(string connectionString)
        {
            productRepo = new ProductRepo(connectionString);
            orderDetailRepo = new OrderDetailRepo(connectionString);
            orderRepo = new OrderRepo(connectionString);
        }
        public List<CartItem> GetCartItems()
        {
            return cart;
        }
        public CartItem GetCartItem(int productID)
        {
            return cart.Find(i => i.Product.ProductId == productID);
        }
        public bool AddItem(int productID)
        {
            Product product = productRepo.GetProductByID(productID);
            if (product == null)
            {
                return false;
            }
            CartItem cartItem = cart.Find(i => i.Product.ProductId == productID);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            return true;
        }
        public bool RemoveItem(int productID)
        {
            Product product = productRepo.GetProductByID(productID);
            if (product == null)
            {
                return false;
            }
            if (cart.RemoveAll(item => item.Product.ProductId == productID) <= 0)
            {
                return false;
            }
            return true;
        }
        public bool EditItem(int productID, int quantity)
        {
            Product product = productRepo.GetProductByID(productID);
            if (product == null)
            {
                return false;
            }
            CartItem cartItem = cart.Find(i => i.Product.ProductId == productID);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                CartItem item = new CartItem()
                {
                    Product = product,
                    Quantity = quantity
                };
                cart.Add(item);
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool Pay()
        {
            if (cart.Count > 0)
            {
                int total = 0;
                int quantity = 0;

                Order order = new Order()
                {
                    OrderId = orderRepo.GetMaxOrderID() + 1,
                    MemberId = 1, // just for admin so I choose member 1 as default
                    Freight = 1000,
                    OrderDate = DateTime.Now,
                    ShippedDate = DateTime.Now.AddDays(10.0),//default 10 days after order
                    RequiredDate = DateTime.Now.AddDays(10.0),
                };
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                cart.ForEach(cartItem =>
                {
                    quantity += cartItem.Quantity;
                    total += (int)cartItem.Product.UnitPrice * cartItem.Quantity;
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = cartItem.Product.ProductId,
                        UnitPrice = cartItem.Product.UnitPrice,
                        Quantity = cartItem.Quantity,
                        Discount = 0
                    };
                    // minus product unit by order quantity in cart
                    Product product = productRepo.getProductById(cartItem.Product.ProductId);
                    product.UnitInStock -= cartItem.Quantity;
                    productRepo.UpdateProductInfo(product);
                    productRepo.SaveChanges();
                    orderDetails.Add(orderDetail);
                    orderDetailRepo.AddNewOrderDetail(orderDetail);
                });
                order.OrderDetails = orderDetails;
                orderRepo.AddNewOrder(order);
                //orderRepo.SaveChanges();
                cart.Clear();
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
