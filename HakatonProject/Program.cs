using System.Text;
using HakatonProject.Controllers;
using HakatonProject.Data;
using HakatonProject.Models.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var _universalKey = "AKJS-189A-1293-KLZQJAHSDJHAJSHHHJZHXKCKHKZXHKCHK"u8.ToArray();

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

var globalLogger = loggerFactory.CreateLogger("Global"); //Глобальный логгер, для вывода юзаем имено его

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(loggerFactory);
builder.Services.AddSingleton(globalLogger);

builder.Services.AddScoped<CurrentUserService>();

builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<FacultiesRepository>();
builder.Services.AddScoped<InterestRepository>();
builder.Services.AddScoped<PlaceRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ChartDataController>();

// Add services to the container.
var connectionAuthString = builder.Configuration.GetConnectionString("AuthConnection") ??
                           throw new InvalidOperationException("Connection string 'AuthConnection' not found.");

var connectionDataString = builder.Configuration.GetConnectionString("DataConnection") ??
                           throw new InvalidOperationException("Connection string 'DataConnection' not found.");

builder.Services.AddDbContext<ApplicationAuthDbContext>(options =>
    options.UseSqlite(connectionAuthString)); //коннект бд для аутентификации/авторизации, создана стандартная

builder.Services.AddDbContext<ApplicationDataDbContext>(options => options.UseSqlite(connectionDataString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";   // куда редиректить, если не авторизован
        options.AccessDeniedPath = "/Home/denied"; // при запрещённом доступе
        options.ExpireTimeSpan = TimeSpan.FromHours(6);
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataDb = scope.ServiceProvider.GetRequiredService<ApplicationDataDbContext>();
    dataDb.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); //мб удалить, если неправильно будет отрабатывать сертификат
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllers(); //коннектим пути в контроллерах для работы Route и прямых указаний алиасов REST-запросов

app.MapRazorPages()
    .WithStaticAssets();

app.Run();