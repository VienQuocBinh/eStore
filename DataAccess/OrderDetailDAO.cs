using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO : BaseDAO
    {
        private static OrderDetailDAO Instance = null;
        private static readonly object locker = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO getInstance
        {
            get
            {
                lock (locker)
                {
                    if (Instance == null)
                    {
                        Instance = new OrderDetailDAO();
                    }
                    return Instance;
                }
            }
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return context.OrderDetails.ToList();
        }
        public List<OrderDetail> GetOrderDetailsByOrderID(int orderId)
        {
            return context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
        }

        public void AddNewOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
        }

        public void updateOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetails.Update(orderDetail);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetails.Remove(orderDetail);
        }
    }
}
