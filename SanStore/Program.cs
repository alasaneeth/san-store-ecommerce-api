using SanStore.Infrastructure;
using SanStore.Application;
using Microsoft.EntityFrameworkCore;
using SanStore.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationService();

builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options=>
{
    options.AddPolicy("CustomePolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#region Database Connectivity
builder.Services.AddDbContext<ApplicationDbContext>(options=> 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options=>
{

}).AddEntityFrameworkStores<ApplicationDbContext>();

#endregion End of Database Connectivity

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
