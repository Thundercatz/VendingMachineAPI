using VendingMachineAPI.Interfaces;
using VendingMachineAPI.Services;
using Microsoft.EntityFrameworkCore;
using VendingMachineAPI.Data;
using System.Globalization; // Your DbContext namespace

var builder = WebApplication.CreateBuilder(args);


//Set invariant culture globally
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
//Console.WriteLine("Globalization-Invariant Mode: " + System.Globalization.CultureInfo.CurrentCulture.Name);


// Add services to the container.
builder.Services.AddControllers();

// Register DbContext with SQL Server
builder.Services.AddDbContext<VendingMachineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register the VendingMachine as a singleton service
//builder.Services.AddSingleton<IVendingMachineClient, VendingMachine>();
//builder.Services.AddSingleton<IVendingMachineOperator, VendingMachine>();
//builder.Services.AddSingleton<IVendingMachineMaintenance, VendingMachine>();
//builder.Services.AddSingleton<IVendingMachineInventory, VendingMachine>();

//Since you're using VendingMachineDbContext, which is a scoped service, you can make VendingMachine a scoped service as well.
//This will align the lifetimes and allow the VendingMachine class to work correctly with DbContext.
builder.Services.AddScoped<IVendingMachineClient, VendingMachine>();
builder.Services.AddScoped<IVendingMachineOperator, VendingMachine>();
builder.Services.AddScoped<IVendingMachineMaintenance, VendingMachine>();
builder.Services.AddScoped<IVendingMachineInventory, VendingMachine>();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
