using ApiPruebaTecnica.ApiDATA.Daos;
using ApiPruebaTecnica.ApiSERVICES.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Agregar la cadena de conexion al servicio
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var conec = new SqlConnection(builder.Configuration.GetConnectionString("ConnectionStrings"));
    conec.Open();
    return conec;
});

builder.Services.AddScoped<ISolicitudService, SolicitudService>();

builder.Services.AddScoped<IDAOSolicitudes, DAOSolicitudes>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
