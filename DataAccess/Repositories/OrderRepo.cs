using BusinessObject;

namespace DataAccess.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        public OrderRepo() { }
        public OrderRepo(String ConnetionString)
        {
            OrderDAO.ConnectionString = ConnetionString;
        }

        public void AddNewOrder(Order order)
        {
            OrderDAO.getInstance.AddNewOrder(order);
        }

        public void DeleteOrder(Order order)
        {
            OrderDAO.getInstance.DeleteOrder(order);
        }

        public Order GetOrderByID(int id)
        {
            return OrderDAO.getInstance.GetOrderByID(id);
        }

        public List<Order> GetOrders()
        {
            return OrderDAO.getInstance.GetOrders();
        }

        public bool SaveChanges()
        {
            return OrderDAO.getInstance.SaveChanges();
        }

        public void UpdateOrderInfo(Order order)
        {
            OrderDAO.getInstance.UpdateOrderInfo(order);
        }

        public List<Order> getOrderbetweenDate(DateTime startDate, DateTime endDate)
        {
            return OrderDAO.getInstance.getOrderbetweenDates(startDate, endDate);
        }

        public List<Order> GetOrdersbyMemID(int id)
        {
            return OrderDAO.getInstance.GetOrdersbyMemID(id);
        }
        public int GetMaxOrderID()
        {
            List<Order> orders = OrderDAO.getInstance.GetOrders();
            return orders.LastOrDefault().OrderId;
        }
    }
}
