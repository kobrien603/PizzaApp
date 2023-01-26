using PizzaApp.Server.Extensions;
using PizzaApp.Server.Middleware;
using PizzaApp.Server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();

builder.Services.AddAuthentication();
builder.Services.ConfigureMySQLDatabase(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddScoped<CustomAuthMiddleware>();
builder.Services.AddScoped<AuthUserService>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger(); // turn off so you don't go directly to api doc
app.UseSwaggerUI();

app.UseMigrationsEndPoint();
app.UseWebAssemblyDebugging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger(); // turn off so you don't go directly to api doc
    //app.UseSwaggerUI();

    //app.UseMigrationsEndPoint();
    //app.UseWebAssemblyDebugging();
}
else
{
    //app.UseExceptionHandler("/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

// middleware
app.UseMiddleware<CustomAuthMiddleware>();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
