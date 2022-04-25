using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public OrdersController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOrders")]
        [EnableQuery]
        public IEnumerable<Order> Get()
        {
            var orders = new List<Order>();
            orders.Add(new Order { Id = 1, Name = "A", Amount = 100.0, TenantId = "X" });
            orders.Add(new Order { Id = 2, Name = "B", Amount = 200.0, TenantId = "Y" });
            orders.Add(new Order { Id = 3, Name = "C", Amount = 200.0, TenantId = "Z" });
            orders.Add(new Order { Id = 4, Name = "D", Amount = 300.0, TenantId = "Z" });
            return orders;
        }
    }
}
