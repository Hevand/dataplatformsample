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
    public class CustomersController : ControllerBase
    {
        private readonly Models.dbAdventureWorksContext _context;

        public CustomersController(Models.dbAdventureWorksContext context)
        {
            _context = context;

        }
                
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ReadModels.Customer>>> GetCustomers()
        {
            string tenantId = "";
            var tenantIdClaims = User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/tenantid");
            if (tenantIdClaims.Any())
            {
                tenantId = tenantIdClaims.FirstOrDefault().Value;
            }

            var result = from c in _context.Customers
                         select new ReadModels.Customer()
                         {
                             Id = c.CustomerId,
                             FriendlyName = !string.IsNullOrEmpty(c.FirstName) ? c.FirstName : c.LastName,
                             FullName = string.Join(" ", c.Title, c.FirstName, c.LastName, c.Suffix),
                             EmailAddress = c.EmailAddress,
                             BillingAddress = from a in c.CustomerAddresses
                                              where a.Address != null 
                                              && "MAIN OFFICE".Equals(a.AddressType, StringComparison.OrdinalIgnoreCase) 
                                              select new CustomerAddress()
                                              {
                                                  AddressId = a.AddressId,
                                                  AddressLine1 = a.Address.AddressLine1,
                                                  AddressLine2 = a.Address.AddressLine2,
                                                  City = a.Address.City,
                                                  PostalCode = a.Address.PostalCode,
                                                  CountryRegion = a.Address.CountryRegion,
                                                  StateProvence = a.Address.StateProvince
                                              },

                             ShippingAddress = from a in c.CustomerAddresses
                                              where a.Address != null
                                              && "SHIPPING".Equals(a.AddressType, StringComparison.OrdinalIgnoreCase)
                                              select new CustomerAddress()
                                              {
                                                  AddressId = a.AddressId,
                                                  AddressLine1 = a.Address.AddressLine1,
                                                  AddressLine2 = a.Address.AddressLine2,
                                                  City = a.Address.City,
                                                  PostalCode = a.Address.PostalCode,
                                                  CountryRegion = a.Address.CountryRegion,
                                                  StateProvence = a.Address.StateProvince
                                              }
                         };

            return await result.ToListAsync();
        }
    }
}
