using Microsoft.AspNetCore.Identity;
using SupCountBE.Core.Entities;
using SupCountBE.Infrastacture;
using SupCountBE.Application;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfpRegisterServices(builder.Configuration).AppRegisterServices();
builder.Services.AddIdentity<User, ApplicationRole>().AddEntityFrameworkStores<SupCountDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.InitializeUserAndRole();

await app.RunAsync();
