using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HakatonProject.Data;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

var globalLogger = loggerFactory.CreateLogger("Global"); //Глобальный логгер, для вывода юзаем имено его

builder.Services.AddSingleton(loggerFactory);
builder.Services.AddSingleton(globalLogger);

// Add services to the container.
var connectionAuthString = builder.Configuration.GetConnectionString("AuthConnection") ??
                       throw new InvalidOperationException("Connection string 'AuthConnection' not found.");

var connectionDataString = builder.Configuration.GetConnectionString("DataConnection") ??
                           throw new InvalidOperationException("Connection string 'DataConnection' not found.");

builder.Services.AddDbContext<ApplicationAuthDbContext>(options =>
    options.UseSqlite(connectionAuthString)); //коннект бд для аутентификации/авторизации, создана стандартная

builder.Services.AddDbContext<ApplicationDataDbContext>(options => options.UseSqlite(connectionDataString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationAuthDbContext>();

builder.Services.AddControllersWithViews();

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
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllers(); //коннектим пути в контроллерах для работы Route и прямых указаний алиасов REST-запросов

app.MapRazorPages()
    .WithStaticAssets();

app.Run();