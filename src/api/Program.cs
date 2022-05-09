using api.Lib;
using api.Models;
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
builder.Services.AddTransient<ITenant, Tenant>();
builder.Services.AddDbContext<dbAdventureWorksContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseODataRouteDebug();

app.Run();


IEdmModel GetModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Address>("Addresses");
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<CustomerAddress>("CustomerAddresses");
    builder.EntitySet<Order>("Orders");

    return builder.GetEdmModel();
}