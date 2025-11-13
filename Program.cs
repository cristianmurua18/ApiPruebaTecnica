using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Agregar la cadena de conexion al servicio
//builder.Services.AddScoped<IDbConnection>(sp =>
//{
//    var conec = new SqlConnection(builder.Configuration.GetConnectionString("SqlConnection"));
//    conec.Open();
//    return conec;
//});

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
