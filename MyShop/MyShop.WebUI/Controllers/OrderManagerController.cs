using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService OrderService)
        {
            this.orderService = OrderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            
            return View(orders);
        }

        public ActionResult UpdateOrder(string Id)    //== Edit order; samo statusot ke se update-ira
        {
            ViewBag.StatusList = new List<string>()     //Moze i da se vnesat vo baza, pa od tamu da se citaat
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(Id);

            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder (Order updatedOrder,string Id)
        {
            Order order = orderService.GetOrder(Id);

            order.OrderStatus = updatedOrder.OrderStatus;

            orderService.UpdateOrder(order);

            return RedirectToAction("index");
        }
    }
}