using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        IOrderRepo orderRepo = new OrderRepo(GetConnectionString());
        IMemberRepo memberRepo = new MemberRepo(GetConnectionString());
        IOrderDetailRepo orderDetailRepo = new OrderDetailRepo(GetConnectionString());
        IProductRepo productRepo = new ProductRepo(GetConnectionString());
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration["ConnectionStrings:Database"];
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Order> orders = orderRepo.GetOrders();
            return View(orders);
        }

        [HttpPost]
        public ActionResult PostusingParameter(DateTime startdate, DateTime enddate)
        {
            ViewData["startDate"] = startdate.ToString("yyyy-MM-dd");
            ViewData["endDate"] = enddate.ToString("yyyy-MM-dd");
            //    DateTime start = DateTime.Parse(startdate);
            //    DateTime end = DateTime.Parse(enddate);
            List<Order> list = orderRepo.getOrderbetweenDate(startdate, enddate);
            return View("Index", list);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRepo.GetOrderByID(id);
            List<OrderDetail> orderDetails = orderDetailRepo.GetOrderDetailsByOrderID(id);
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Product = productRepo.GetProductByID(orderDetail.ProductId);
            }
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.od = orderDetails;
            return View(order);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            List<Member> members = memberRepo.GetMembers();
            List<int> memberID = new List<int>();
            foreach (Member member in members)
            {
                memberID.Add(member.MemberId);
            }
            ViewBag.Members = memberID;

            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            List<Member> members = memberRepo.GetMembers();
            List<int> memberID = new List<int>();
            foreach (Member member in members)
            {
                memberID.Add(member.MemberId);
            }
            ViewBag.Members = memberID;
            try
            {
                if (order.OrderDate >= order.ShippedDate || order.OrderDate >= order.RequiredDate)
                {
                    ViewBag.CreateError = "Order date must be before shipped date and required date";
                    throw new Exception();
                }
                orderRepo.AddNewOrder(order);
                orderRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            Order order = orderRepo.GetOrderByID(id);
            List<Member> members = memberRepo.GetMembers();
            List<int> memberID = new List<int>();
            foreach (Member member in members)
            {
                order.MemberId = member.MemberId;
                memberID.Add(member.MemberId);
            }
            ViewBag.Members = memberID;

            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            List<Member> members = memberRepo.GetMembers();
            foreach (var member in members)
            {
                if (member.MemberId == order.MemberId)
                {
                    order.Member = member;
                    break;
                }

            }

            List<int> memberID = new List<int>();
            foreach (Member member in members)
            {
                order.MemberId = member.MemberId;
                memberID.Add(member.MemberId);
            }
            ViewBag.Members = memberID;
            try
            {
                if (order.OrderDate >= order.ShippedDate || order.OrderDate >= order.RequiredDate)
                {
                    ViewBag.UpdateError = "Order date must be before shipped date and required date";
                    throw new Exception();
                }
                orderRepo.UpdateOrderInfo(order);
                orderRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            Order order = orderRepo.GetOrderByID(id);
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Order order = orderRepo.GetOrderByID(id);
                List<OrderDetail> orderDetails = orderDetailRepo.GetOrderDetails();
                // search and delete order detail has order id key
                foreach (var orderDetail in orderDetails)
                {
                    if (orderDetail.OrderId == id)
                    {
                        orderDetailRepo.DeleteOrderDetail(orderDetail);
                    }
                }
                orderDetailRepo.SaveChanges();
                orderRepo.DeleteOrder(order);
                orderRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
