using api.Lib;
using api.Models;
using api.ReadModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Added oData Service
builder.Services.AddControllers().AddOData(options => options
    .Select()
    .Filter()
    .OrderBy()
    .AddRouteComponents("odata",GetModel())
);


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITenantService, TenantService>();
builder.Services.AddDbContext<dbTenantAdminContext>();
builder.Services.AddDbContext<dbAdventureWorksContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Challenge:   Natigate the odata model and operations, the basic URL must be valid.
// Objective:   Use the hostname of the API Management gateway, if used.
// Approach:    APIM to define host name as request header. Middleware to validate and update context.
//              E.g. Forwarded: proto=https;host=temp.org/product/v2;"
app.Use((context, next) =>
{
    if (context.Request.Headers.ContainsKey("Forwarded"))
    {
        IEnumerable<string> pairs = context.Request.Headers["Forwarded"].ToString().Split(';');

        foreach(string pair in pairs)
        {
            if (pair.StartsWith("proto=", StringComparison.OrdinalIgnoreCase))
            {
                context.Request.Scheme = pair.Substring("proto=".Length);
            }
            else if (pair.StartsWith("host="))
            {
                context.Request.Host = new HostString(pair.Substring("host=".Length));
            }
        }
    }
    return next(context);
});


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseODataRouteDebug();

app.Run();


IEdmModel GetModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<api.ReadModels.Customer>("Customers");
    builder.EntitySet<api.ReadModels.Order>("Orders");

    return builder.GetEdmModel();
}