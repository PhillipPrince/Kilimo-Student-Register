using Kilimo.DBConnection;
using Kilimo.DBOperations;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register SqlConnectionManager and SQLOperations with appropriate dependencies
builder.Services.AddScoped<SqlConnectionManager>(provider =>
{
    return new SqlConnectionManager(connectionString);
});

builder.Services.AddScoped<SQLOperations>(provider =>
{
    var sqlConnectionManager = provider.GetRequiredService<SqlConnectionManager>();
    return new SQLOperations(sqlConnectionManager);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
