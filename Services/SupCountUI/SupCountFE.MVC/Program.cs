using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.Services.Implementations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;                 
    options.Cookie.IsEssential = true;             
});

builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("ApiSetting"));

builder.Services.AddTransient<ApiSecurity>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IJustificationService, JustificationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserGroupService, UserGroupService>();
builder.Services.AddTransient<IReimbursementService, ReimbursementService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IParticipationService, ParticipationService>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



builder.Services.AddSingleton<Helper>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google-auth:ClientId"]!;
    options.ClientSecret = builder.Configuration["Google-auth:ClientSecret"]!;
    options.Scope.Add("email");
    options.SaveTokens = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSession(); 

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();

    // Skip check for /auth/login
    if (path == "/auth/login" || path == "/account/login" || path == "/account/googleResponse")
    {
        await next();
        return;
    }

    var token = context.Session.GetString("JWTToken");
    var expiryString = context.Session.GetString("TokenExpiry");

    if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(expiryString) ||
        (DateTime.TryParse(expiryString, out var expiryTime) && expiryTime < DateTime.UtcNow))
    {
        context.Response.Redirect("/auth/login");
        return;
    }

    await next();
});

app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
