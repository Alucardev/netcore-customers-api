using CustomerApi.UseCases;
using CustomersApi.CasosDeUso;  
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddCors();


//añadimos la implementacion del repositorio al DI
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//agregamos el contexto
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
builder.Services.AddDbContext<CustomerDbContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true)
                 .AllowCredentials());

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

