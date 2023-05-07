using Microsoft.EntityFrameworkCore;
using Psinder.Api.Models;
using Psinder.Api.Services;

var builder = WebApplication.CreateBuilder(args);

using (PsinderContext _context = new())
{
    //bool usuniete = await _context.Database.EnsureDeletedAsync();
    //Console.WriteLine($"Usuniêto bazê danych: {usuniete}");
    bool utworzone = await _context.Database.EnsureCreatedAsync();
    Console.WriteLine($"Utworzono bazê danych: {utworzone}");
}
// Add services to the container.
builder.WebHost.UseUrls("https://localhost:5003");
builder.Services.AddControllers();
builder.Services.AddDbContext<PsinderContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddTransient<IPetService, PetService>();

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
