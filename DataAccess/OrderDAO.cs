using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class OrderDAO : BaseDAO
    {
        private static OrderDAO Instace = null;
        private static readonly object locker = new object();
        private OrderDAO() { }
        public static OrderDAO getInstance
        {
            get
            {
                lock (locker)
                {
                    if (Instace == null)
                    {
                        Instace = new OrderDAO();
                    }
                    return Instace;
                }
            }
        }
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration["ConnectionStrings:Database"];
        }
        public List<Order> GetOrders()
        {
            return context.Orders.AsNoTracking().Include(member => member.Member).ToList();
        }

        public Order GetOrderByID(int orderID)
        {
            return context.Orders.AsNoTracking().Include(member => member.Member).FirstOrDefault(o => o.OrderId == orderID);
        }

        public void AddNewOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Order] ON");
                    context.Orders.Add(order);
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Order] OFF");
                    transaction.Commit();
                }
            }
        }

        public void UpdateOrderInfo(Order order)
        {
            var o = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (o != null)
            {
                o.Member = order.Member;
                o.MemberId = order.MemberId;
                o.OrderDate = order.OrderDate;
                o.RequiredDate = order.RequiredDate;
                o.ShippedDate = order.ShippedDate;
                o.Freight = order.Freight;
                context.Orders.Update(o);
            }
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
        }

        public List<Order> getOrderbetweenDates(DateTime startDate, DateTime endDate)
        {
            return context.Orders.Where(
                order => order.OrderDate > startDate && order.OrderDate < endDate
                ).ToList();
        }

        public List<Order> GetOrdersbyMemID(int mid)
        {
            return context.Orders.Where(a => a.MemberId == mid).ToList();
        }
    }
}
