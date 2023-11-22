using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepo
    {
        public List<OrderDetail> GetOrderDetails();
        public List<OrderDetail> GetOrderDetailsByOrderID(int orderID);
        public void AddNewOrderDetailList(List<OrderDetail> orderDetailList);

        public void AddNewOrderDetail(OrderDetail orderDetail);
        public void UpdateOrderDetail(OrderDetail orderDetail);
        public void DeleteOrderDetail(OrderDetail orderDetail);
        public bool SaveChanges();
    }
}
