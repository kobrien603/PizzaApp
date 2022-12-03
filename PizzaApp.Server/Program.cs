using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL;

var builder = WebApplication.CreateBuilder(args);

// add database
string connectionString = builder.Configuration.GetConnectionString("PizzaConnection");
builder.Services.AddDbContextFactory<PizzaContext>(options =>
{
	options.UseMySql(
		connectionString,
		ServerVersion.AutoDetect(connectionString),
		x => x.MigrationsAssembly("PizzaApp.Server")
	);
});

builder.Services.AddDbContext<PizzaContext>(options =>
{
	options.UseMySql(
		connectionString,
		ServerVersion.AutoDetect(connectionString),
		x => x.MigrationsAssembly("PizzaApp.Server")
	);
});

builder.Services.AddEntityFrameworkMySql();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
