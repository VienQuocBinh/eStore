using BusinessObject;

namespace DataAccess.Repositories
{
    public interface IOrderRepo
    {
        public List<Order> GetOrders();
        public Order GetOrderByID(int id);
        public void AddNewOrder(Order order);
        public void UpdateOrderInfo(Order order);
        public void DeleteOrder(Order order);
        public bool SaveChanges();
        public List<Order> getOrderbetweenDate(DateTime startDate, DateTime endDate);
        public List<Order> GetOrdersbyMemID(int id);
        public int GetMaxOrderID();
    }
}
