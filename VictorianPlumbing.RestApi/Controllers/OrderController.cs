using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VictorianPlumbing.Data;
using VictorianPlumbing.Data.Models;
using VictorianPlumbing.RestApi.Models;

namespace VictorianPlumbing.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Orders = new[]
        {
            "{ \"customerName\": \"Emma\", \"items\": [ { \"productId\": \"67890\", \"quantity\": 2, \"cost\": 10.99 }, { \"productId\": \"54321\", \"quantity\": 1, \"cost\": 5.99 } ] }"
        };

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [AcceptVerbs("POST")]
        public IActionResult CreateOrder([FromBody] string orderJson)
        {
            var order = JsonSerializer.Deserialize<OrderDto>(orderJson);

            // Validation
            if (order == null)
            {
                return BadRequest("Order can't be null!");
            }

            if (string.IsNullOrEmpty(order.CustomerName))
            {
                return BadRequest("Customer name is required.");
            }

            if (order.Items == null)
            {
                return BadRequest("You need an item to proceed with the order.");
            }

            AddOrder(order);

            return Ok();
        }

        public void AddOrder(OrderDto order)
        {
            var customer = new Customer();
            customer.Name = order.CustomerName;

            //Add customer to SQL Database
            using (var context = new MyDbContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            //Add data to data model
            var newOrder = new Order();
            newOrder.OrderDate = DateTime.Now;
            newOrder.CustomerId = customer.Id;

            foreach (var item in order.Items)
            {
                newOrder.Items.Add(new OrderItem { ProductId = item.productId, Quantity = item.quantity, Cost = item.cost });
            }

            //Add order and items to SQL Database
            using (var context = new MyDbContext())
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
            }
        }
    }
}