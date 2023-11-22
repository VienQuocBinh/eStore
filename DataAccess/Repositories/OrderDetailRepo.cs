using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public OrderDetailRepo() { }
        public OrderDetailRepo(String connectionString)
        {
            OrderDetailDAO.ConnectionString = connectionString;
        }
        public void AddNewOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.getInstance.AddNewOrderDetail(orderDetail);
        }

        public void AddNewOrderDetailList(List<OrderDetail> orderDetailList)
        {
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.getInstance.DeleteOrderDetail(orderDetail);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return OrderDetailDAO.getInstance.GetOrderDetails();
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            return OrderDetailDAO.getInstance.GetOrderDetailsByOrderID(orderID);
        }

        public bool SaveChanges()
        {
            return OrderDetailDAO.getInstance.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            OrderDetailDAO.getInstance.updateOrderDetail(orderDetail);
        }
    }
}
