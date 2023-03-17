using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using IdentityServer4_Demo.Contexts;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<AspNetIdentityContext>(option => {
    option.UseSqlServer(configuration.GetConnectionString("IdentityStorageConnection"), opt => opt.MigrationsAssembly(assembly));
});

builder.Services.AddIdentity<IdentityUser , IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(option =>
    {
        option.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("OperationStorageConnection"),opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("OperationStorageConnection"), opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddIdentityServer()
//        .AddTestUsers(Users.GetUsers())
//        .AddConfigurationStore(option =>
//        {
//            option.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("OperationStorageConnection"));
//        });

builder.Services.AddDbContext<ApplicationDBContext>(option => {
    option.UseSqlServer(configuration.GetConnectionString("OperationStorageConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
