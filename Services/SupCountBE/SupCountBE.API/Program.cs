using Microsoft.AspNetCore.Identity;
using SupCountBE.Core.Entities;
using SupCountBE.Infrastacture;
using SupCountBE.Application;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.API.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options=>
{
    options.EnableEndpointRouting = false;
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfpRegisterServices(builder.Configuration).AppRegisterServices();
builder.Services.AppRegisterServices();
builder.Services.AddIdentity<User, ApplicationRole>().AddEntityFrameworkStores<SupCountDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Set to true in production
    x.SaveToken = false;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        ClockSkew = TimeSpan.Zero 
    };
})
.AddGoogle(options =>
{
    var googleConfig = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleConfig["ClientId"]!;
    options.ClientSecret = googleConfig["ClientSecret"]!;
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAllOrigins");
app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<ConnectedUserMiddleware>();

app.MapControllers();

await app.InitializeUserAndRole();

await app.RunAsync();
