using Microsoft.EntityFrameworkCore;
using Psinder.Api.Models;
using Psinder.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:5003");
builder.Services.AddControllers();
builder.Services.AddDbContext<PsinderContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<IPetService, PetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "DELETE", "PUT");
    options.WithOrigins("https://localhost:5001");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
