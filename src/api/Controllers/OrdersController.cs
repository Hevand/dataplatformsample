using api.ReadModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Models.dbAdventureWorksContext _context;

        public OrdersController(Models.dbAdventureWorksContext context)
        {
            _context = context;

        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ReadModels.Order>>> GetOrders()
        {
            string tenantId = "";
            var tenantIdClaims = User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/tenantid");
            if (tenantIdClaims.Any())
            {
                tenantId = tenantIdClaims.FirstOrDefault().Value;
            }

            var result = from o in _context.SalesOrderHeaders
                         select new Order()
                         {
                             CustomerId = o.CustomerId,
                             Id = o.SalesOrderId,
                             OrderDate = o.OrderDate,
                             ShipDate = o.ShipDate,
                             TotalDue = o.TotalDue
                         };
                         
                         

            return await result.ToListAsync();
        }
    }
}
