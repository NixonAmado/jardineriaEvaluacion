using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.AddAplicationServices();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddDbContext<PracticasTokenContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("conexMySql");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
