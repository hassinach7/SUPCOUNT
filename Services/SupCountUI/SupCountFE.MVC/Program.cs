using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SupCountFE.MVC.Models;
using SupCountFE.MVC.Services.Contracts;
using SupCountFE.MVC.Services.Implementations;
using System.Reflection;
using System.Security.Claims;

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
    options.ClientId = "44523278386-h40s3qr6ii6f6flqsi5mtb2ht0jlk28d.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.Scope.Add("email");
    options.SaveTokens = true;
    //options.Events.OnCreatingTicket = async context =>
    //{
    //    var email = context.Principal.FindFirst(ClaimTypes.Email)?.Value;
    //    Console.WriteLine($"StartUp : [AUTH] Email utilisateur connecté : {email}");

    //   // inject IAuthService and call login action 
    //    options.Events.OnCreatingTicket = async context =>
    //    {
    //        var email = context.Principal!.FindFirst(ClaimTypes.Email)?.Value;
    //        // call login action



    //    };

    //};
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//builder.Services.AddTransient<IAuthService, AuthService>();

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
